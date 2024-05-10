public class BlobBrain : Brain
{
    private void Awake()
    {
        ProtectAction.OnActivate += ProtectAction_OnActivate;

        ChangeState(_idle);
    }

    private void OnDestroy() => ProtectAction.OnActivate -= ProtectAction_OnActivate;

    private void ProtectAction_OnActivate(bool activated)
    {
        if (activated) ChangeState(_protect);
        else ChangeState(_idle);
    }
}