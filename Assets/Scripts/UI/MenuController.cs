using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : GameManagerStateListener
{
    private const float COUNTDOWN_FIX = 0.2f;

    [Header("Panels")]
    [SerializeField] private GameObject playersPanel = default;
    [SerializeField] private GameObject creditsPanel = default;
    
    
    [Header("Countdown")]
    [SerializeField] private float startDelay = default;
    [SerializeField] private GameObject countdown = default;
    [SerializeField] private Image countdownFill;
    [SerializeField] private TextMeshProUGUI countdownText;
    
    [Header("Players")]
    [SerializeField] private RectTransform playerGrid = default;
    [SerializeField] private GameObject playerPanelLeftPrefab = default;
    [SerializeField] private GameObject playerPanelRightPrefab = default;

    private List<PlayerInfo> players;
    private bool startingGame;
    private float startCountdown;

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenCredits()
    {
        playersPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        playersPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

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
            startCountdown = startDelay;
            countdown.SetActive(true);
            UpdateCountdown();
        }

        if (startingGame)
        {
            startCountdown -= Time.deltaTime;
            UpdateCountdown();

            if (startCountdown <= -COUNTDOWN_FIX)
            {
                StartGame();
            }
        }
    }

    void UpdateCountdown()
    {
        countdownFill.fillAmount = 1 - startCountdown / startDelay;
        countdownText.text =  $"{Mathf.Ceil(startCountdown)}";
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
        var prefab = players.Count % 2 == 0 ? playerPanelLeftPrefab : playerPanelRightPrefab;
        var playerPanelGameObject = Instantiate(prefab, playerGrid);
        playerPanelGameObject.GetComponent<PlayerPanel>().SetPlayerInfo(playerInfo);
        players.Add(playerInfo);
    }
}