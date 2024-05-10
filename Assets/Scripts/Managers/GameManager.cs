using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Modules")]
    [SerializeField] private List<Module> _modules = new();

    [Header("Prefabs")]
    [SerializeField] private GameObject _blobPrefab;
    [SerializeField] private GameObject _asteroidPrefab;

    [Header("Dependencies")]
    [SerializeField] private TMP_Text _chargesNumber;

    [Header("Settings")]
    [SerializeField] private int _maxDestructionCharges = 3;

    public Blob Blob { get; private set; }
    public bool CanTouch { get; set; }
    public int DestructionCharges { get; private set; }

    // Prefabs
    public GameObject BlobPrefab => _blobPrefab;
    public GameObject AsteroidPrefab => _asteroidPrefab;

    // Modules
    private int _initializedModules = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        DestructionCharges = _maxDestructionCharges;

        Module.OnModuleLoaded += Module_OnModuleLoaded;
        WavesModule.OnWaveEnd += WavesModule_OnWaveEnd;
    }

    private void OnDestroy()
    {
        Module.OnModuleLoaded -= Module_OnModuleLoaded;
        WavesModule.OnWaveEnd -= WavesModule_OnWaveEnd;
    }

    private void Update() => _chargesNumber.text = $"x {DestructionCharges}";

    public T Mod<T>() where T : Module => _modules.OfType<T>().First();
    private void SpawnBlob() => Blob = Instantiate(BlobPrefab).GetComponent<Blob>();
    public void RemoveCharge() => DestructionCharges--;

    public void AddCharge()
    {
        DestructionCharges++;
        DestructionCharges = Mathf.Clamp(DestructionCharges, 0, _maxDestructionCharges);
    }

    private void WavesModule_OnWaveEnd(bool win)
    {
        if (win) AddCharge();
        else DestructionCharges = _maxDestructionCharges;
    }

    private void Module_OnModuleLoaded()
    {
        _initializedModules++;
        if (_initializedModules == _modules.Count) CompleteInit();
    }

    private void CompleteInit()
    {
        SpawnBlob();
        Mod<WavesModule>().StartWave();
        Debug.Log("<color=yellow>Game Manager init completed.</color>");
    }
}