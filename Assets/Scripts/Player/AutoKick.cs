using UnityEngine;

public class AutoKick : IKick, ICanSendEvent
{
    GameObject _player;
    float _force;

    public AutoKick(GameObject player, float force)
    {
        _player = player;
        _force = force;
    }

    public void Kick()
    {
        if (GameManager.Instance != null)
        {
            BallController farestBall = GameManager.Instance.FindFarestBall(_player);
            if (farestBall != null)
            {
                Transform nearestGoal = GameManager.Instance.FindNearestGoal(farestBall.gameObject);
                if (nearestGoal != null)
                {
                    Vector3 direction = nearestGoal.position - farestBall.transform.position;
                    farestBall.OnKick(direction, _force);
                    this.Publish(new BallKickEvent(farestBall));
                }
            }
        }
    }
}
