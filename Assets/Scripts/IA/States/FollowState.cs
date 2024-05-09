using UnityEngine;

public class FollowState : State
{
    private Vector3 _targetPosition;

    public override void OnEnter(Brain brain)
    {
        base.OnEnter(brain);
        _targetPosition = GameManager.Instance.Blob.transform.position;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _brain.transform.position = Vector3.MoveTowards(_brain.transform.position, _targetPosition, (_brain as AsteroidBrain).Speed * Time.deltaTime);
    }
}