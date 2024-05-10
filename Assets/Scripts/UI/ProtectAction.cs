using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProtectAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event Action<bool> OnActivate;

    public void OnPointerDown(PointerEventData eventData) => OnActivate?.Invoke(true);
    public void OnPointerUp(PointerEventData eventData) => OnActivate?.Invoke(false);
}