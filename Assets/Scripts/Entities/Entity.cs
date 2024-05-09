using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static event Action<Entity> OnDeath;

    [SerializeField] protected SpriteRenderer _renderer;

    protected float _health;

    private void Death()
    {
        OnDeath?.Invoke(this);
    }
}