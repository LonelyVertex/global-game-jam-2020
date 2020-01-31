using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Menu = 0,
        Game = 1,
        Launch = 2
    }
    
    public delegate void StateChanged(State newState);
    public event StateChanged OnStateChange;

    private static GameManager instance;
    public static GameManager Instance => instance;
    
    private State state = State.Menu;
    public State CurrentState => state;

    private void Awake()
    {
        instance = this;
    }

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
