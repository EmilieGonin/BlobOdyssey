using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WavesModule : Module
{
    public static event Action OnWaveStart;
    public static event Action<bool> OnWaveEnd;

    [Header("Settings")]
    [SerializeField] private int _enemiesPerWave = 10;
    [SerializeField] private float _minSpawnCooldown = 1;
    [SerializeField] private float _maxSpawnCooldown = 3;

    [Header("Events")]
    [SerializeField] private UnityEvent OnGameOver;
    [SerializeField] private UnityEvent OnVictory;

    public int EnemiesSpawned => _enemiesSpawned.Count;
    public int WaveNumber { get; private set; } = 1;

    private int _enemiesKilled;
    private Coroutine _spawner;
    private List<GameObject> _enemiesSpawned;

    private void Awake()
    {
        Entity.OnDeath += Entity_OnDeath;
        PowerPopup.OnEmotionSelect += StartWave;
        GameOverPopup.OnRestart += RestartWaves;
    }

    private void OnDestroy()
    {
        Entity.OnDeath -= Entity_OnDeath;
        PowerPopup.OnEmotionSelect -= StartWave;
        GameOverPopup.OnRestart -= RestartWaves;
    }

    private void Entity_OnDeath(Entity entity)
    {
        if (entity is Blob)
        {
            GameOver();
            return;
        }

        _enemiesKilled++;

        if (_enemiesKilled == _enemiesPerWave) Victory();
    }

    public void RestartWaves()
    {
        WaveNumber = 1;
        StartWave();
    }

    public void StartWave()
    {
        OnWaveStart?.Invoke();
        _spawner = StartCoroutine(SpawnEnemies());
    }

    public void StartWave(EmotionType emotion = EmotionType.Joy) => StartWave();

    private IEnumerator SpawnEnemies()
    {
        _enemiesSpawned = new();
        _enemiesKilled = 0;

        while (_enemiesSpawned.Count < _enemiesPerWave)
        {
            GameObject asteroid = Instantiate(_manager.AsteroidPrefab);
            _enemiesSpawned.Add(asteroid);
            yield return new WaitForSeconds(UnityEngine.Random.Range(_minSpawnCooldown, _maxSpawnCooldown));
        }

        yield return null;
    }

    private void Victory()
    {
        WaveNumber++;
        OnWaveEnd?.Invoke(true);
        OnVictory?.Invoke();
    }

    private void GameOver()
    {
        if (_spawner != null) StopCoroutine(_spawner);
        foreach (var asteroid in _enemiesSpawned) if (asteroid != null) Destroy(asteroid);

        OnWaveEnd?.Invoke(false);
        OnGameOver?.Invoke();
    }
}