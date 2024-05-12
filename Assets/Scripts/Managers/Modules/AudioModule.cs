using UnityEngine;

public class AudioModule : Module
{
    [SerializeField] AudioSource _source;

    [Header("Audio Clips")]
    [SerializeField] AudioClip _destroy;
    [SerializeField] AudioClip _absorb;
    [SerializeField] AudioClip _damage;
    [SerializeField] AudioClip _gameOver;
    [SerializeField] AudioClip _victory;
    [SerializeField] AudioClip _restart;

    private void Awake()
    {
        DestroyAction.OnActivate += DestroyAction_OnActivate;
        Asteroid.OnAbsorb += Asteroid_OnAbsorb;
        Asteroid.OnDamageInflicted += Asteroid_OnDamageInflicted;
        WavesModule.OnWaveStart += WavesModule_OnWaveStart;
        WavesModule.OnWaveEnd += WavesModule_OnWaveEnd;
        ProtectAction.OnProtectToggle += ProtectAction_OnProtectToggle;
        Blob.OnProtectBroke += Blob_OnProtectBroke;
    }

    private void OnDestroy()
    {
        DestroyAction.OnActivate -= DestroyAction_OnActivate;
        Asteroid.OnAbsorb -= Asteroid_OnAbsorb;
        Asteroid.OnDamageInflicted -= Asteroid_OnDamageInflicted;
        WavesModule.OnWaveStart -= WavesModule_OnWaveStart;
        WavesModule.OnWaveEnd -= WavesModule_OnWaveEnd;
        ProtectAction.OnProtectToggle -= ProtectAction_OnProtectToggle;
        Blob.OnProtectBroke -= Blob_OnProtectBroke;
    }

    private void DestroyAction_OnActivate() => _source.PlayOneShot(_destroy);
    private void Asteroid_OnAbsorb(Asteroid obj) => _source.PlayOneShot(_absorb);
    private void Asteroid_OnDamageInflicted(float obj) => _source.PlayOneShot(_damage);
    private void WavesModule_OnWaveStart() => _source.PlayOneShot(_restart);

    private void WavesModule_OnWaveEnd(bool win)
    {
        if (win) _source.PlayOneShot(_victory);
        else _source.PlayOneShot(_gameOver);
    }

    private void ProtectAction_OnProtectToggle(bool toggle)
    {
        if (toggle) _source.Play();
        else if (_source.isPlaying) _source.Stop();
    }

    private void Blob_OnProtectBroke() => _source.Stop();
}