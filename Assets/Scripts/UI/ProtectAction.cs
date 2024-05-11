using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProtectAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TMP_Text _cooldown;
    [SerializeField] private Button _button;

    public static event Action<bool> OnProtectToggle;

    private bool _canActivate = true;
    private bool _isActivated = false;

    private void Awake() => Blob.OnProtectBroke += SetCooldown;
    private void OnDestroy() => Blob.OnProtectBroke -= SetCooldown;

    private void SetCooldown()
    {
        _isActivated = false;
        _canActivate = false;
        _button.interactable = false;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        int cooldown = 5;

        while (cooldown > 0)
        {
            yield return new WaitForSeconds(1);
            cooldown -= 1;
        }

        _canActivate = true;
        _button.interactable = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_canActivate) return;
        OnProtectToggle?.Invoke(true);
        _isActivated = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isActivated) return;
        OnProtectToggle?.Invoke(false);
        _isActivated = false;
    }
}