using System;
using UnityEngine;

public class Module : MonoBehaviour
{
    public static event Action OnModuleLoaded;

    protected GameManager _manager => GameManager.Instance;

    private void Start() => OnModuleLoaded?.Invoke();
}