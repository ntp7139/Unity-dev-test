using UnityEngine;

public class NormalKick : IKick
{
    float _force;
    GameObject _ball;
    Vector3 _playerForward;

    public NormalKick(Vector3 playerForward, GameObject ball, float force)
    {
        _playerForward = playerForward;
        _ball = ball;
        _force = force;
    }

    public void Kick()
    {
        if (_ball != null)
        {
            BallController ballController = _ball.GetComponent<BallController>();
            if (ballController != null)
            {
                if (GameManager.Instance != null)
                {
                    Transform nearestGoal = GameManager.Instance.FindNearestGoal(_ball);
                    if (nearestGoal != null)
                    {
                        Vector3 direction = (nearestGoal.position - _ball.transform.position);
                        Vector2 direction2D = new Vector2(direction.x, direction.z);
                        Vector2 playerForward2D = new Vector2(_playerForward.x, _playerForward.z);
                        float angle = Mathf.Abs(Vector2.SignedAngle(playerForward2D, direction2D));
                        if (angle <= 120)
                        {
                            ballController.OnKick(direction, _force);
                            this.Publish(new BallKickEvent(ballController));
                        }
                    }
                }
            }
        }
    }
}
