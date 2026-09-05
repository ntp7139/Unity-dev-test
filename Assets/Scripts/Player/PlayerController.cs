using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject modelPlayer;
    Vector3 _prevDirection;
    StateController _stateController;
    IdleState _idleState;
    RunState _runState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator != null)
        {
            _idleState = new IdleState(animator, this);
            _runState = new RunState(animator, this);
            _stateController = new StateController(_idleState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotationModel(_prevDirection);
        if (_stateController != null)
        {
            _stateController.UpdateState();
        }
    }

    public void Move(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            transform.position = transform.position + direction * speed * Time.deltaTime;
            _prevDirection = direction;
        }
    }

    void UpdateRotationModel(Vector3 direction)
    {
        if (modelPlayer != null)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion targetAngle = Quaternion.Euler(0, angle, 0);
            modelPlayer.transform.rotation = Quaternion.Lerp(
                modelPlayer.transform.rotation,
                targetAngle,
                1
            );
        }
    }

    public void ChangeIdleState()
    {
        if (_stateController != null && _idleState != null)
        {
            _stateController.ChangeState(_idleState);
        }
    }

    public void ChangeRunState()
    {
        if (_stateController != null && _runState != null)
        {
            _stateController.ChangeState(_runState);
        }
    }
}
