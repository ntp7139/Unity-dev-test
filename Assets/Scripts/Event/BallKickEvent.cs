public struct BallKickEvent : IEvent
{
    public BallController Ball;

    public BallKickEvent(BallController ball)
    {
        Ball = ball;
    }
}
