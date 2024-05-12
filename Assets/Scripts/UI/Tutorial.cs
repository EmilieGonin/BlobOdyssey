using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static event Action OnComplete;

    public void Complete() => OnComplete?.Invoke();
}