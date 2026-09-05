public class StateController
{
    IState _currentState;

    public StateController(IState beginState)
    {
        _currentState = beginState;
    }

    public void ChangeState(IState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void UpdateState()
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }
}
