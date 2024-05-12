using AYellowpaper.SerializedCollections;
using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static event Action<Entity> OnDeath;

    [Header("Dependencies")]
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected SerializedDictionary<EmotionType, Sprite> _sprites;

    protected virtual void Death()
    {
        OnDeath?.Invoke(this);
        if (this is Asteroid) Destroy(gameObject);
    }
}