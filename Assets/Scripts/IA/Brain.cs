using UnityEngine;

public class Brain : MonoBehaviour
{
    protected State _currentState;

    // States
    protected IdleState _idle = new();
    protected ProtectState _protect = new();
    protected FollowState _follow = new();

    private void Update() => _currentState?.OnUpdate();

    public void ChangeState(State state)
    {
        _currentState?.OnExit();
        _currentState = state;
        _currentState.OnEnter(this);
    }
}