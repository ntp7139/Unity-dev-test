using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ICanListenEvent, ICanSendEvent
{
    [SerializeField]
    int timeCongrats = 2;
    public static GameManager Instance;
    public List<GoalObject> goalObjects;
    public List<BallController> balls;
    public List<BallController> doneBalls;
    public EventSubscription _ballKickEvent;
    public EventSubscription _ballGoalEvent;

    public void RegisterOnLoad(List<GoalObject> goalObjects, List<BallController> balls)
    {
        this.goalObjects = goalObjects;
        this.balls = balls;
        doneBalls = new List<BallController>();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }

    void OnEnable()
    {
        _ballKickEvent = this.Listen<BallKickEvent>(OnBallKick);
        _ballGoalEvent = this.Listen<OnGoalEvent>(OnBallGoal);
    }

    void Start()
    {
        doneBalls = new List<BallController>();
    }

    void OnBallGoal(OnGoalEvent evt)
    {
        StartCoroutine(WaitForCongrats());
    }

    IEnumerator WaitForCongrats()
    {
        yield return new WaitForSeconds(timeCongrats);
        this.Publish(new OnCongratsCompleteEvent());
        if (CheckCompleteGame())
        {
            this.Publish(new OnCompleteGameEvent());
        }
    }

    void OnBallKick(BallKickEvent evt)
    {
        if (evt.Ball != null)
        {
            if (balls.Contains(evt.Ball))
            {
                balls.Remove(evt.Ball);
            }
            if (!doneBalls.Contains(evt.Ball))
            {
                doneBalls.Add(evt.Ball);
            }
        }
    }

    bool CheckCompleteGame()
    {
        if (balls.Count == 0)
        {
            return true;
        }
        return false;
    }

    public Transform FindNearestGoal(GameObject ball)
    {
        if (goalObjects == null || goalObjects.Count == 0)
        {
            return null;
        }
        float nearestDistance = int.MaxValue;
        Transform nearestGoal = null;
        foreach (GoalObject goalObject in goalObjects)
        {
            if (goalObject != null)
            {
                float distance = Vector3.SqrMagnitude(
                    goalObject.TargetPos.position - ball.transform.position
                );
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestGoal = goalObject.TargetPos;
                }
            }
        }
        return nearestGoal;
    }

    public BallController FindFarestBall(GameObject player)
    {
        if (balls == null || balls.Count == 0)
        {
            return null;
        }
        float farestDistance = 0;
        BallController farestBall = null;
        foreach (BallController ball in balls)
        {
            if (ball != null)
            {
                float distance = Vector3.SqrMagnitude(
                    player.transform.position - ball.transform.position
                );
                if (distance > farestDistance)
                {
                    farestDistance = distance;
                    farestBall = ball;
                }
            }
        }
        return farestBall;
    }
}
