using System;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Entity
{
    [Header("Stats")]
    [SerializeField] private float _maxHealth = 20;

    public Dictionary<EmotionType, Emotion> Emotions { get; private set; }

    private float _currentHealth;

    private void Awake()
    {
        WavesModule.OnWaveStart += WavesModule_OnWaveStart;
        Asteroid.OnDamageInflicted += Asteroid_OnDamageInflicted;
        Asteroid.OnAbsorb += Asteroid_OnAbsorb;

        _currentHealth = _maxHealth;
        InitEmotions();
    }

    private void InitEmotions()
    {
        Emotions = new();

        foreach (var emotion in Enum.GetValues(typeof(EmotionType)))
        {
            Emotions[(EmotionType)emotion] = new Emotion((EmotionType)emotion);
        }
    }

    private void OnDestroy()
    {
        WavesModule.OnWaveStart -= WavesModule_OnWaveStart;
        Asteroid.OnDamageInflicted -= Asteroid_OnDamageInflicted;
        Asteroid.OnAbsorb -= Asteroid_OnAbsorb;
    }

    private void WavesModule_OnWaveStart()
    {
        _currentHealth = _maxHealth;
    }

    private void Asteroid_OnDamageInflicted(float damage)
    {
        TakeDamage(damage);
        LoseEmotions();
        if (IsDead()) Death();
    }

    private void Asteroid_OnAbsorb(Asteroid asteroid)
    {
        float damage = (asteroid.Damage / 2) / Emotions[EmotionType.Joy].Level;
        TakeDamage(damage);
        LoseEmotions();

        Emotions[asteroid.Emotion].Add(20);
        Debug.Log($"<color=lime>Gain +20% of {asteroid.Emotion}!</color>");
        if (IsDead()) Death();
    }

    private void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log($"<color=red>Lost {damage} HP !</color>");
    }

    private void LoseEmotions()
    {
        foreach (var emotion in Enum.GetValues(typeof(EmotionType)))
        {
            Emotions[(EmotionType)emotion].Substract(10);
        }

        Debug.Log($"<color=red>Lost -10% of each emotions !</color>");
    }

    private bool IsDead()
    {
        if (_currentHealth <= 0) return true;
        bool isDead = false;

        foreach (var emotion in Enum.GetValues(typeof(EmotionType)))
        {
            if (Emotions[(EmotionType)emotion].IsOver)
            {
                isDead = true;
                break;
            }
        }

        return isDead;
    }

    protected override void Death()
    {
        base.Death();
        InitEmotions();
    }
}