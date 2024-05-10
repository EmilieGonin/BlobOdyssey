using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Entity
{
    [SerializeField] private ProgressBar _healthBar;

    [Header("Stats")]
    [SerializeField] private float _maxHealth = 20;
    [SerializeField] private int _baseEmotionGain = 20;
    [SerializeField] private int _baseEmotionLose = 5;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth { get; private set; }
    public Dictionary<EmotionType, Emotion> Emotions { get; private set; }
    public List<EmotionType> StrongestEmotions { get; private set; } = new();

    private Coroutine _regen;

    private void Awake()
    {
        WavesModule.OnWaveEnd += WavesModule_OnWaveEnd;
        Asteroid.OnDamageInflicted += Asteroid_OnDamageInflicted;
        Asteroid.OnAbsorb += Asteroid_OnAbsorb;
        ProtectAction.OnActivate += ToggleProtection;
        PowerPopup.OnEmotionSelect += LevelUpEmotion;

        InitHealth();
        InitEmotions();
    }

    private void OnDestroy()
    {
        WavesModule.OnWaveEnd -= WavesModule_OnWaveEnd;
        Asteroid.OnDamageInflicted -= Asteroid_OnDamageInflicted;
        Asteroid.OnAbsorb -= Asteroid_OnAbsorb;
        ProtectAction.OnActivate -= ToggleProtection;
        PowerPopup.OnEmotionSelect -= LevelUpEmotion;
    }

    private void Update() => _healthBar.UpdateValue((CurrentHealth / MaxHealth * 100) / 100);

    #region Health
    private void InitHealth() => CurrentHealth = _maxHealth;

    private void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, _maxHealth);
        LoseEmotions();
    }
    #endregion

    #region Emotions
    public void InitEmotions()
    {
        Emotions = new();

        foreach (EmotionType emotion in Enum.GetValues(typeof(EmotionType)))
        {
            Emotions[emotion] = new Emotion(emotion);
        }

        SetEmotion();
    }

    private void GainEmotion(EmotionType emotion) => Emotions[emotion].Add(_baseEmotionGain);

    private void LoseEmotions()
    {
        foreach (var emotion in Enum.GetValues(typeof(EmotionType)))
        {
            Emotions[(EmotionType)emotion].Substract(_baseEmotionLose);
        }
    }

    private void SetEmotion(EmotionType currentEmotion = EmotionType.Joy)
    {
        StrongestEmotions.Clear();
        float maxEmotionValue = 0;

        foreach (EmotionType emotion in Enum.GetValues(typeof(EmotionType)))
        {
            float emotionValue = Emotions[emotion].Value;

            if (emotionValue > maxEmotionValue)
            {
                maxEmotionValue = emotionValue;
                StrongestEmotions.Clear();
                StrongestEmotions.Add(emotion);
            }
            else if (emotionValue == maxEmotionValue)
            {
                StrongestEmotions.Add(emotion);
            }
        }

        if (!StrongestEmotions.Contains(currentEmotion)) currentEmotion = StrongestEmotions[0];

        _renderer.color = EmotionPalette.GetColor(currentEmotion);
    }

    private void LevelUpEmotion(EmotionType emotion)
    {
        Emotions[emotion].LevelUp();

        switch (emotion)
        {
            case EmotionType.Sadness:
                _maxHealth += 10;
                InitHealth();
                break;
            case EmotionType.Fear:
                GameManager.Instance.AddCharge();
                break;
        }
    }
    #endregion

    #region Protect Action
    private void ToggleProtection(bool activated)
    {
        if (activated) _regen = StartCoroutine(Regen());
        else if (_regen != null) StopCoroutine(_regen);
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
    #endregion

    private void Asteroid_OnDamageInflicted(float damage)
    {
        TakeDamage(damage);
        SetEmotion();
        if (IsDead()) Death();
    }

    private void Asteroid_OnAbsorb(Asteroid asteroid)
    {
        float damage = (asteroid.Damage / 2) / Emotions[EmotionType.Joy].Level;
        TakeDamage(damage);
        GainEmotion(asteroid.Emotion);
        SetEmotion(asteroid.Emotion);
        if (IsDead()) Death();
    }

    private void WavesModule_OnWaveEnd(bool win)
    {
        InitHealth();
        if (!win) InitEmotions();
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
}