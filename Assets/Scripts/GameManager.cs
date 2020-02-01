using UnityEngine;

public class GameManager : StaticAccess<GameManager>
{
    public enum State
    {
        Menu = 0,
        Game = 1,
        Launch = 2,
        GameOver = 3
    }

    [SerializeField] private float timeLimit = default;
    
    public delegate void StateChanged(State newState);
    public event StateChanged OnStateChange;
    
    private State state = State.Menu;
    public State CurrentState => state;

    private PlayerInfo winner;
    public PlayerInfo Winner => winner;

    private float currentTime;
    public float CurrentTime => currentTime;

    private bool playersCanJoin = true;
    public bool PlayersCanJoin => playersCanJoin;

    void Update()
    {
        if (state == State.Game)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                state = State.GameOver;
                NotifyStateChange();
            }
        }
    }

    public void LockPlayers()
    {
        playersCanJoin = false;
    }

    public void StartGame()
    {
        state = State.Game;
        currentTime = timeLimit;
        NotifyStateChange();
    }
    
    public void LaunchRocket(PlayerInfo player)
    {
        winner = player;
        state = State.Launch;
        NotifyStateChange();
    }

    void NotifyStateChange()
    {
        OnStateChange?.Invoke(state);
    }
}
