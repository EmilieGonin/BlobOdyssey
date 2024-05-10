using UnityEngine;

public class AsteroidBrain : Brain
{
    [SerializeField] private int _speed = 3;

    public int Speed => _speed;

    private void Awake() => ChangeState(_follow);
}