using TMPro;
using UnityEngine;

public class EmotionBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _level;

    private EmotionType _emotion;

    private void Awake() => WavesModule.OnWaveStart += WavesModule_OnWaveStart;
    private void OnDestroy() => WavesModule.OnWaveStart -= WavesModule_OnWaveStart;

    public void Init(EmotionType emotion)
    {
        _emotion = emotion;
        _name.text = emotion.ToString();
    }

    private void WavesModule_OnWaveStart() => _level.text = $"Lvl. {GameManager.Instance.Blob.Emotions[_emotion].Level}";
}