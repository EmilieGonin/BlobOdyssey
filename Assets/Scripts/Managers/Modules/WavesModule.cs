using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesModule : Module
{
    [Header("Settings")]
    [SerializeField] private int _enemiesPerWave = 10;
    [SerializeField] private float _minSpawnCooldown = 1;
    [SerializeField] private float _maxSpawnCooldown = 3;

    private int _enemiesKilled;
    private int _waveNumber = 1;
    private Coroutine _spawner;
    private List<GameObject> _enemiesSpawned;

    private void Awake()
    {
        Entity.OnDeath += Entity_OnDeath;
    }

    private void OnDestroy()
    {
        Entity.OnDeath -= Entity_OnDeath;
    }

    private void Entity_OnDeath(Entity entity)
    {
        if (entity is Blob)
        {
            GameOver();
            return;
        }

        _enemiesKilled++;

        if (_enemiesKilled == _enemiesPerWave) NextWave();
    }

    public void StartWave()
    {
        _spawner = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        _enemiesSpawned = new();
        _enemiesKilled = 0;

        while (_enemiesSpawned.Count < _enemiesPerWave)
        {
            GameObject asteroid = Instantiate(_manager.AsteroidPrefab);
            _enemiesSpawned.Add(asteroid);
            yield return new WaitForSeconds(Random.Range(_minSpawnCooldown, _maxSpawnCooldown));
        }

        yield return null;
    }

    private void NextWave()
    {
        _waveNumber++;
        StartWave();
    }

    private void GameOver()
    {
        if (_spawner != null) StopCoroutine(_spawner);
        foreach (var asteroid in _enemiesSpawned) if (asteroid != null) Destroy(asteroid);

        StartWave();
    }
}