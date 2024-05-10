public class ProtectState : State
{
    public override void OnEnter(Brain brain)
    {
        base.OnEnter(brain);
        GameManager.Instance.CanTouch = false;
    }
}