using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static event Action<Entity> OnDeath;

    [Header("Dependencies")]
    [SerializeField] protected SpriteRenderer _renderer;

    protected virtual void Death()
    {
        OnDeath?.Invoke(this);
        if (this is Asteroid) Destroy(gameObject);
        if (this is Blob) (this as Blob).InitEmotions();
    }
}