using UnityEngine;

public class Blob : Entity
{
    [Header("Stats")]
    [SerializeField] private float _maxHealth = 20;

    private float _currentHealth;

    private void Awake()
    {
        WavesModule.OnWaveStart += WavesModule_OnWaveStart;
        Asteroid.OnDamageInflicted += Asteroid_OnDamageInflicted;
        _currentHealth = _maxHealth;
    }

    private void OnDestroy()
    {
        WavesModule.OnWaveStart -= WavesModule_OnWaveStart;
        Asteroid.OnDamageInflicted -= Asteroid_OnDamageInflicted;
    }

    private void WavesModule_OnWaveStart()
    {
        _currentHealth = _maxHealth;
    }

    private void Asteroid_OnDamageInflicted(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Death();
    }
}