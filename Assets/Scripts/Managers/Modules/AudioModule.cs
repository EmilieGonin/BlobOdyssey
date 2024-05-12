using UnityEngine;

public class AudioModule : Module
{
    [SerializeField] AudioSource _source;

    [Header("Audio Clips")]
    [SerializeField] AudioClip _destroy;

    private void Awake()
    {
        DestroyAction.OnActivate += DestroyAction_OnActivate;
    }

    private void OnDestroy()
    {
        DestroyAction.OnActivate -= DestroyAction_OnActivate;
    }

    private void DestroyAction_OnActivate()
    {
        _source.PlayOneShot(_destroy);
    }
}