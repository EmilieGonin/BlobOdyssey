using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PowerPopup : MonoBehaviour, IPointerDownHandler
{
    public static event Action<EmotionType> OnEmotionSelect;

    private EmotionType _emotion;

    public void Init(EmotionType emotion)
    {
        _emotion = emotion;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnEmotionSelect?.Invoke(_emotion);
    }
}