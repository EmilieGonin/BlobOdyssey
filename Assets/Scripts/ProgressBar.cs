using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private Image _image;

    [Range(0, 1), OnValueChanged("OnValueChangedCallback"), ProgressBar("Value", EColor.Red)]
    [SerializeField] private float _value = 0.5f;

    public float Value => _value;

    void Update() => UpdateBar();
    private void OnValueChangedCallback() => UpdateBar();
    private void UpdateBar() => _image.fillAmount = _value;
    public void UpdateValue(float value) => _value = value;
    public void ToggleHUD(bool toggle) => _canvas.alpha = toggle ? 1 : 0;
}