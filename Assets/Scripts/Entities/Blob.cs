using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Entity
{
    [SerializeField] private ProgressBar _healthBar;

    [Header("Stats")]
    [SerializeField] private float _maxHealth = 20;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth { get; private set; }
    public Dictionary<EmotionType, Emotion> Emotions { get; private set; }

    private Coroutine _regen;

    private void Awake()
    {
        WavesModule.OnWaveStart += InitHealth;
        Asteroid.OnDamageInflicted += Asteroid_OnDamageInflicted;
        Asteroid.OnAbsorb += Asteroid_OnAbsorb;
        ProtectAction.OnActivate += ProtectAction_OnActivate;

        InitHealth();
        InitEmotions();
    }

    private void OnDestroy()
    {
        WavesModule.OnWaveStart -= InitHealth;
        Asteroid.OnDamageInflicted -= Asteroid_OnDamageInflicted;
        Asteroid.OnAbsorb -= Asteroid_OnAbsorb;
        ProtectAction.OnActivate -= ProtectAction_OnActivate;
    }

    private void Update() => _healthBar.UpdateValue((CurrentHealth / MaxHealth * 100) / 100);

    private void InitHealth() => CurrentHealth = _maxHealth;
    private void LoseEmotions() { foreach (var emotion in Enum.GetValues(typeof(EmotionType))) Emotions[(EmotionType)emotion].Substract(10); }
    private void GainEmotion(EmotionType emotion) => Emotions[emotion].Add(20);

    public void InitEmotions()
    {
        Emotions = new();

        foreach (EmotionType emotion in Enum.GetValues(typeof(EmotionType)))
        {
            Emotions[emotion] = new Emotion(emotion);
        }

        SetEmotion();
    }

    private void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
    }

    private bool IsDead()
    {
        if (CurrentHealth <= 0) return true;
        bool isDead = false;

        foreach (EmotionType emotion in Enum.GetValues(typeof(EmotionType)))
        {
            if (Emotions[emotion].IsOver)
            {
                isDead = true;
                break;
            }
        }

        return isDead;
    }

    private IEnumerator Regen()
    {
        while (true)
        {
            CurrentHealth += (1 * Emotions[EmotionType.Fear].Level);
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
            yield return new WaitForSeconds(1);
        }
    }

    private void Asteroid_OnDamageInflicted(float damage)
    {
        TakeDamage(damage);
        LoseEmotions();
        SetEmotion();
        if (IsDead()) Death();
    }

    private void Asteroid_OnAbsorb(Asteroid asteroid)
    {
        float damage = (asteroid.Damage / 2) / Emotions[EmotionType.Joy].Level;
        TakeDamage(damage);
        LoseEmotions();
        GainEmotion(asteroid.Emotion);
        SetEmotion(asteroid.Emotion);
        if (IsDead()) Death();
    }

    private void ProtectAction_OnActivate(bool activated)
    {
        if (activated) _regen = StartCoroutine(Regen());
        else if (_regen != null) StopCoroutine(_regen);
    }

    private void SetEmotion(EmotionType currentEmotion = EmotionType.Joy)
    {
        foreach (EmotionType emotion in Enum.GetValues(typeof(EmotionType)))
        {
            if (Emotions[emotion].Value > Emotions[currentEmotion].Value) currentEmotion = emotion;
        }

        _renderer.color = EmotionPalette.GetColor(currentEmotion);
    }
}