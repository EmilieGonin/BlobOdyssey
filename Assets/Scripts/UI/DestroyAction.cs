using System;
using UnityEngine;
using UnityEngine.UI;

public class DestroyAction : MonoBehaviour
{
    public static event Action OnActivate;

    [SerializeField] private Button _button;
    [SerializeField] private Image _chargesImage;
    [SerializeField] private Sprite[] _sprites;

    private void Awake() => GameManager.OnChargeGain += UpdateChargeNumber;
    private void OnDestroy() => GameManager.OnChargeGain -= UpdateChargeNumber;
    
    private void Update()
    {
        _button.interactable = GameManager.Instance.DestructionCharges > 0;
        _chargesImage.gameObject.SetActive(GameManager.Instance.DestructionCharges > 0);
    }

    public void Destroy()
    {
        if (!GameManager.Instance.CanTouch) return;
        GameManager.Instance.RemoveCharge();

        UpdateChargeNumber();
        OnActivate?.Invoke();
    }

    private void UpdateChargeNumber()
    {
        int charges = GameManager.Instance.DestructionCharges;
        _chargesImage.sprite = charges > 0 ? _sprites[charges - 1] : _sprites[0];
    }
}