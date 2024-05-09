public class IdleState : State
{
    public override void OnEnter(Brain brain)
    {
        base.OnEnter(brain);
        GameManager.Instance.CanTouch = true;
    }
}