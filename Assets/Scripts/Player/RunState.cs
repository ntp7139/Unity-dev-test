using UnityEngine;

public class RunState : IState
{
    Animator _animator;
    PlayerController _player;

    public RunState(Animator animator, PlayerController player)
    {
        _player = player;
        _animator = animator;
    }

    public void Enter()
    {
        if (_animator != null)
        {
            _animator.SetFloat("Speed", 1);
        }
    }

    public void Update()
    {
        if (_player != null)
        {
            Vector3 direction = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.back;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.forward;
            }
            if (direction != Vector3.zero)
            {
                _player.Move(direction);
            }
            else
            {
                _player.ChangeIdleState();
            }
        }
    }

    public void Exit() { }
}
