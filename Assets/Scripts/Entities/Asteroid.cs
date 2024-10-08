using NaughtyAttributes;
using System;
using UnityEngine;

public class Asteroid : Entity
{
    public static event Action<float> OnDamageInflicted;
    public static event Action<Asteroid> OnAbsorb;

    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField, Tag] private string _blobTag;

    [Header("Stats")]
    [SerializeField] private float _baseDamage = 5;
    [SerializeField] private float _bigSizeChance = 10;

    public float Damage { get; private set; }
    public EmotionType Emotion { get; private set; }

    private void Awake()
    {
        DestroyAction.OnActivate += Death;

        Emotion = (EmotionType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EmotionType)).Length);

        SetAsteroidSize();
        SetPositionOutsideViewport();

        _renderer.sprite = _sprites[Emotion];
        ParticleSystem.MainModule main = _particles.main;
        main.startColor = EmotionPalette.GetColor(Emotion);
        _trail.startColor = EmotionPalette.GetColor(Emotion);

        gameObject.name = $"Asteroid of {Enum.GetName(typeof(EmotionType), Emotion)} [#{GameManager.Instance.Mod<WavesModule>().EnemiesSpawned + 1}]";
    }

    private void OnDestroy() => DestroyAction.OnActivate -= Death;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(_blobTag)) return;
        OnDamageInflicted?.Invoke(Damage);
        Death();
    }

    private void OnMouseDown()
    {
        if (!GameManager.Instance.CanTouch) return;
        OnAbsorb?.Invoke(this);
        Death();
    }

    private void SetAsteroidSize()
    {
        bool isBig = UnityEngine.Random.Range(1, 100) < _bigSizeChance;
        Damage = isBig ? _baseDamage * 2 : _baseDamage;
        if (isBig) transform.localScale = new(1.5f, 1.5f, 1.5f);
    }

    private void SetPositionOutsideViewport()
    {
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        int randomSide = UnityEngine.Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (randomSide)
        {
            case 0: // bottom
                spawnPosition = new Vector3(UnityEngine.Random.Range(bottomLeft.x, topRight.x), bottomLeft.y - 1, 0);
                break;
            case 1: // top
                spawnPosition = new Vector3(UnityEngine.Random.Range(bottomLeft.x, topRight.x), topRight.y + 1, 0);
                break;
            case 2: // left
                spawnPosition = new Vector3(bottomLeft.x - 1, UnityEngine.Random.Range(bottomLeft.y, topRight.y), 0);
                break;
            case 3: // right
                spawnPosition = new Vector3(topRight.x + 1, UnityEngine.Random.Range(bottomLeft.y, topRight.y), 0);
                break;
        }

        transform.position = spawnPosition;
    }
}