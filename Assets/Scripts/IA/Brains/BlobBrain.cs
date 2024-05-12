using System;
using UnityEngine;

public class BlobBrain : Brain
{
    public static event Action<State> OnStateChange;

    private void Awake()
    {
        ProtectAction.OnProtectToggle += ToggleProtection;
        Blob.OnProtectBroke += Blob_OnProtectBroke;

        ChangeState(_idle);
    }

    private void OnDestroy() => ProtectAction.OnProtectToggle -= ToggleProtection;

    private void ToggleProtection(bool activated)
    {
        if (activated) ChangeState(_protect);
        else ChangeState(_idle);
    }

    private void Blob_OnProtectBroke() => ToggleProtection(false);

    public override void ChangeState(State state)
    {
        base.ChangeState(state);
        //Debug.Log($"Blob new state : {state}");
        OnStateChange?.Invoke(state);
    }
}