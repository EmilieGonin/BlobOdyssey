using System;
using TMPro;
using UnityEngine;

public class GameOverPopup : MonoBehaviour
{
    public static event Action OnRestart;

    [SerializeField] private TMP_Text _wavesCompleted;

    private void Awake()
    {
        int waveNumber = GameManager.Instance.Mod<WavesModule>().WaveNumber;
        _wavesCompleted.text = $"You survived {waveNumber} {(waveNumber > 1 ? "waves" : "wave")} !";
    }

    public void Restart() => OnRestart?.Invoke();
}