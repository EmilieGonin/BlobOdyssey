using System;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    public static event Action OnRestart;

    public void Restart() => OnRestart?.Invoke();
    public void Quit() => Application.Quit();
}