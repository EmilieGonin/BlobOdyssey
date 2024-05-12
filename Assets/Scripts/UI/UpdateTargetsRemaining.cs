using TMPro;
using UnityEngine;

public class UpdateTargetsRemaining : MonoBehaviour
{
    [SerializeField] private TMP_Text _targets;

    private int _targetsKilled = 0;
    private int _maxTargets = 10;

    private void Awake()
    {
        WavesModule.OnWaveStart += WavesModule_OnWaveStart;
        Entity.OnDeath += Entity_OnDeath;
    }

    private void OnDestroy()
    {
        WavesModule.OnWaveStart -= WavesModule_OnWaveStart;
        Entity.OnDeath -= Entity_OnDeath;
    }

    private void Update() => _targets.text = $"{_targetsKilled}/{_maxTargets}";

    private void WavesModule_OnWaveStart()
    {
        _maxTargets = GameManager.Instance.Mod<WavesModule>().EnemiesPerWave;
        _targetsKilled = 0;
    }

    private void Entity_OnDeath(Entity obj)
    {
        if (obj is Asteroid) _targetsKilled++;
    }
}