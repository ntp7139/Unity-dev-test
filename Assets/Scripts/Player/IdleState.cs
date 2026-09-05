using UnityEngine;

public class IdleState : IState
{
    Animator _animator;
    PlayerController _player;

    public IdleState(Animator animator, PlayerController player)
    {
        _animator = animator;
        _player = player;
    }

    public void Enter()
    {
        if (_animator != null)
        {
            _animator.SetFloat("Speed", 0);
        }
    }

    public void Update()
    {
        if (_player != null)
        {
            if (
                Input.GetKey(KeyCode.A)
                || Input.GetKey(KeyCode.S)
                || Input.GetKey(KeyCode.W)
                || Input.GetKey(KeyCode.D)
            )
            {
                _player.ChangeRunState();
            }
        }
    }

    public void Exit() { }
}
