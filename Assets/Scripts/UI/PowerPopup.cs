using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerPopup : MonoBehaviour, IPointerUpHandler
{
    public static event Action<EmotionType> OnEmotionSelect;

    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;

    private EmotionType _emotion;

    public void Init(EmotionType emotion)
    {
        _emotion = emotion;
        _name.text = emotion.ToString();
        _name.color = EmotionPalette.GetColor(emotion);
    }

    public void OnPointerUp(PointerEventData eventData) => OnEmotionSelect?.Invoke(_emotion);
}