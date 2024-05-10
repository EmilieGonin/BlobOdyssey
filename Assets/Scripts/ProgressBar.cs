using NaughtyAttributes;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [Range(0, 1), OnValueChanged("OnValueChangedCallback"), ProgressBar("Value", EColor.Red)]
    [SerializeField] private float _value = 0.5f;
    [SerializeField] private RectTransform _rectTransform;

    public float Value => _value;

    void Update() => _rectTransform.localScale = new(_rectTransform.localScale.x, _value, _rectTransform.localScale.z); 
    private void OnValueChangedCallback() => _rectTransform.localScale = new(_rectTransform.localScale.x, _value, _rectTransform.localScale.z);
    public void UpdateValue(float value) => _value = value;
}