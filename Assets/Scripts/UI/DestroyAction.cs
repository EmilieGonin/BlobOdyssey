using System;
using UnityEngine;
using UnityEngine.UI;

public class DestroyAction : MonoBehaviour
{
    public static event Action OnActivate;

    [SerializeField] private Button _button;

    private void Update() => _button.interactable = GameManager.Instance.DestructionCharges > 0;

    public void Destroy()
    {
        if (!GameManager.Instance.CanTouch) return;

        GameManager.Instance.RemoveCharge();
        OnActivate?.Invoke();
    }
}