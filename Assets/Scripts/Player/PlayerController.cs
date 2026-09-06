using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICanListenEvent, ICanSendEvent
{
    [Header("Basic Info")]
    [SerializeField]
    float forceKick = 100f;
    public float ForceKick => forceKick;

    [SerializeField]
    float forceDribble = 100;
    public float ForceDribble => forceDribble;

    [SerializeField]
    float speed = 2f;

    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject modelPlayer;
    public Vector3 PlayerForward => modelPlayer.transform.forward;
    Vector3 _prevDirection;
    StateController _stateController;
    IdleState _idleState;
    RunState _runState;
    IKick _kickAction;

    [SerializeField]
    List<GameObject> _availableBalls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (animator != null)
        {
            _idleState = new IdleState(animator, this);
            _runState = new RunState(animator, this);
            _stateController = new StateController(_idleState);
        }
        _availableBalls = new List<GameObject>();
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
            transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
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

    void OnCollisionEnter(Collision collision)
    {
        if (_stateController != null)
        {
            _stateController.OnCollisionEnter(collision);
        }
    }

    void SetKickAction(IKick kickAction)
    {
        _kickAction = kickAction;
    }

    public void ExcuteNormalKick()
    {
        if (_availableBalls != null && _availableBalls.Count > 0)
        {
            GameObject availableBall = SelectAvailableBall();
            if (availableBall != null)
            {
                IKick normalKick = new NormalKick(PlayerForward, availableBall, forceKick);
                SetKickAction(normalKick);
                Kick();
            }
        }
    }

    public GameObject SelectAvailableBall()
    {
        // ưu tiên góc của bóng  so với hướng của forward nhỏ hơn 150 độ
        // Nếu nhiều hơn 2 quả bóng thoải mãn về góc thì lấy quả bóng gần nhất
        List<GameObject> balls = new List<GameObject>();
        foreach (GameObject ball in _availableBalls)
        {
            if (ball != null)
            {
                Vector3 ballDirection = ball.transform.position - transform.position;
                float angle = Vector3.Angle(PlayerForward, ballDirection);
                if (angle <= 75)
                {
                    balls.Add(ball);
                }
            }
        }
        if (balls.Count == 1)
        {
            return balls[0];
        }
        if (balls.Count > 1)
        {
            float nearestDistance = int.MaxValue;
            GameObject nearestBall = null;
            foreach (GameObject ball in balls)
            {
                if (ball != null)
                {
                    float distance = Vector3.SqrMagnitude(
                        ball.transform.position - transform.position
                    );
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestBall = ball;
                    }
                }
            }
            return nearestBall;
        }
        return null;
    }

    public void SetAvailableBall(GameObject availableBall)
    {
        if (_availableBalls != null && !_availableBalls.Contains(availableBall))
        {
            _availableBalls.Add(availableBall);
        }
        if (_availableBalls.Count == 1)
        {
            this.Publish(new AvailableToKickEvent());
        }
    }

    public void RemoveAvailableBall(GameObject availableBall)
    {
        if (_availableBalls.Contains(availableBall))
        {
            _availableBalls.Remove(availableBall);
        }
        if (_availableBalls.Count == 0)
        {
            this.Publish(new NotAvailableToKickEvent());
        }
    }

    public void ExcuteAutoKick()
    {
        IKick autoKick = new AutoKick(gameObject, forceKick);
        SetKickAction(autoKick);
        Kick();
    }

    public void Kick()
    {
        if (_kickAction != null)
        {
            _kickAction.Kick();
        }
    }
}
