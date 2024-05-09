using UnityEngine;

public class Brain : MonoBehaviour
{
    protected State _currentState;

    private void Update()
    {
        _currentState?.OnUpdate();
    }

    public void ChangeState(State state)
    {
        _currentState?.OnExit();
        _currentState = state;
        _currentState.OnEnter(this);
    }
}