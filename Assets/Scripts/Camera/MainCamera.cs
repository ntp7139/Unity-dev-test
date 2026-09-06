using UnityEngine;

public class MainCamera : MonoBehaviour, ICanListenEvent
{
    public GameObject player;
    GameObject _mainTarget;

    [SerializeField]
    Vector3 distance = new Vector3(0, 6, -1.6077f);
    EventSubscription _onKickball;
    EventSubscription _onGoalCongratsComplete;

    void OnEnable()
    {
        _onKickball = this.Listen<BallKickEvent>(OnBallKick);
        _onGoalCongratsComplete = this.Listen<OnCongratsCompleteEvent>(OnCongratsComplete);
    }

    void OnDisable()
    {
        _onKickball.Dispose();
        _onGoalCongratsComplete.Dispose();
    }

    void Update()
    {
        if (_mainTarget != null)
        {
            transform.position = _mainTarget.transform.position + distance;
        }
    }

    public void OnBallKick(BallKickEvent evt)
    {
        if (evt.Ball != null)
        {
            OnChangeTarget(evt.Ball.gameObject);
        }
    }

    public void OnCongratsComplete(OnCongratsCompleteEvent evt)
    {
        if (player != null)
        {
            OnChangeTarget(player);
        }
    }

    public void OnChangeTarget(GameObject mainTarget)
    {
        if (mainTarget != null)
        {
            _mainTarget = mainTarget;
        }
    }
}
