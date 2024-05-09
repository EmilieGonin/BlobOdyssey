using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Modules")]
    [SerializeField] private List<Module> _modules = new();

    [Header("Prefabs")]
    [SerializeField] private GameObject _blobPrefab;
    [SerializeField] private GameObject _asteroidPrefab;

    public Blob Blob { get; private set; }

    public GameObject BlobPrefab => _blobPrefab;
    public GameObject AsteroidPrefab => _asteroidPrefab;

    private int _initializedModules = 0;

    public T Mod<T>() where T : Module => _modules.OfType<T>().First();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Module.OnModuleLoaded += Module_OnModuleLoaded;
    }

    private void OnDestroy()
    {
        Module.OnModuleLoaded -= Module_OnModuleLoaded;
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

    private void SpawnBlob()
    {
        Blob = Instantiate(BlobPrefab).GetComponent<Blob>();
    }
}