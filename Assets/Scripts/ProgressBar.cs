using NaughtyAttributes;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [Range(0, 1), OnValueChanged("OnValueChangedCallback")]
    [SerializeField] private float _niveauEmotion = 0.5f;
    [SerializeField] private RectTransform _rectTransform;

    void Update()
    {
        _rectTransform.localScale = new Vector3(_rectTransform.localScale.x, _niveauEmotion, _rectTransform.localScale.z); 
    }

    private void OnValueChangedCallback()
    {
        _rectTransform.localScale = new Vector3(_rectTransform.localScale.x, _niveauEmotion, _rectTransform.localScale.z);
    }
}