using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuController : GameManagerStateListener
{
    [SerializeField] private float startDelay = default;
    [SerializeField] private GameObject countdown = default;
    [Header("Players")]
    [SerializeField] private RectTransform playerGrid = default;
    [SerializeField] private GameObject playerPanelPrefab = default;

    private List<PlayerInfo> players;
    private bool startingGame;

    protected override void Start()
    {
        base.Start();
        players = new List<PlayerInfo>();
        LocalMultiplayerManager.Instance.OnPlayerJoined += OnPlayerJoined;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (LocalMultiplayerManager.Instance != null)
        {
            LocalMultiplayerManager.Instance.OnPlayerJoined -= OnPlayerJoined;
        }
    }

    void Update()
    {
        if (!startingGame && players.Count > 1 && players.All(p => p.Ready))
        {
            startingGame = true;
            countdown.SetActive(true);
            Invoke(nameof(StartGame), startDelay);
        }
    }
    
    void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    protected override void OnGameManagerStateChange(GameManager.State newState)
    {
        gameObject.SetActive(newState == GameManager.State.Menu);
    }

    void OnPlayerJoined(PlayerInfo playerInfo)
    {
        var playerPanelGameObject = Instantiate(playerPanelPrefab, playerGrid);
        playerPanelGameObject.GetComponent<PlayerPanel>().SetPlayerInfo(playerInfo);
        players.Add(playerInfo);
    }
}