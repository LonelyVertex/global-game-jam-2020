public class GameManager : StaticAccess<GameManager>
{
    public enum State
    {
        Menu = 0,
        Game = 1,
        Launch = 2
    }
    
    public delegate void StateChanged(State newState);
    public event StateChanged OnStateChange;
    
    private State state = State.Menu;
    public State CurrentState => state;

    public void StartGame()
    {
        state = State.Game;
        NotifyStateChange();
    }

    public void LaunchRocket()
    {
        state = State.Launch;
        NotifyStateChange();
    }

    void NotifyStateChange()
    {
        OnStateChange?.Invoke(state);
    }
}
