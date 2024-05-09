using System;
using UnityEngine;

public enum Emotion
{
    Joy, Sadness, Anger, Fear
}

public class Entity : MonoBehaviour
{
    public static event Action<Entity> OnDeath;

    [Header("Dependencies")]
    [SerializeField] protected SpriteRenderer _renderer;

    protected void Death()
    {
        OnDeath?.Invoke(this);
        if (this is Asteroid) Destroy(gameObject);
    }
}