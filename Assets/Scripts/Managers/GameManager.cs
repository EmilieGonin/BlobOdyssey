using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action OnChargeGain;

    [Header("Modules")]
    [SerializeField] private List<Module> _modules = new();

    [Header("Prefabs")]
    [SerializeField] private GameObject _blobPrefab;
    [SerializeField] private GameObject _asteroidPrefab;

    [Header("Scenes")]
    [SerializeField, Scene] private string _tutoScene;

    [Header("Dependencies")]
    [SerializeField] private TMP_Text _chargesNumber;

    [Header("Settings")]
    [SerializeField] private int _maxDestructionCharges = 3;
    [SerializeField] private float _tutorialTimer = 3;

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
        WavesModule.OnWaveStart += WavesModule_OnWaveStart;

        SceneManager.LoadSceneAsync(_tutoScene, LoadSceneMode.Additive);
    }

    private void OnDestroy()
    {
        Module.OnModuleLoaded -= Module_OnModuleLoaded;
        WavesModule.OnWaveStart -= WavesModule_OnWaveStart;
    }

    private void Update() => _chargesNumber.text = $"x {DestructionCharges}";

    public T Mod<T>() where T : Module => _modules.OfType<T>().First();
    private void SpawnBlob() => Blob = Instantiate(BlobPrefab).GetComponent<Blob>();
    public void RemoveCharge() => DestructionCharges--;

    public void AddCharge()
    {
        DestructionCharges++;
        DestructionCharges = Mathf.Clamp(DestructionCharges, 0, _maxDestructionCharges);
        OnChargeGain?.Invoke();
    }

    private void WavesModule_OnWaveStart()
    {
        if (Mod<WavesModule>().WaveNumber == 1)
        {
            DestructionCharges = _maxDestructionCharges;
            OnChargeGain?.Invoke();
        }
        else AddCharge();
    }

    private void Module_OnModuleLoaded()
    {
        _initializedModules++;
        if (_initializedModules == _modules.Count) CompleteInit();
    }

    private void CompleteInit()
    {
        Debug.Log("<color=yellow>Game Manager init completed.</color>");
        StartCoroutine(StartTutorial());
        SpawnBlob();
    }

    private IEnumerator StartTutorial()
    {
        yield return new WaitForSeconds(_tutorialTimer);
        SceneManager.UnloadSceneAsync(_tutoScene);
        Mod<WavesModule>().StartWave();
        yield return null;
    }
}