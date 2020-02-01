using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIController : GameManagerStateListener
{
    [Header("Panels")]
    [SerializeField] private GameObject gamePanel = default;
    [SerializeField] private GameObject pausePanel = default;
    [SerializeField] private GameObject gameOverPanel = default;
    [SerializeField] private GameObject savedPanel = default;

    [Header("Game Panel")]
    [SerializeField] private TextMeshProUGUI currentTimeText = default;
    [SerializeField] private TextMeshProUGUI currentRocketValue = default;
    
    [Header("Saved Panel")]
    [SerializeField] private Image winnerImage = default;

    private bool paused;

    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void CancelPause()
    {
        Time.timeScale = 1;
        paused = false;
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void OpenMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    protected override void OnGameManagerStateChange(GameManager.State newState)
    {
        gameObject.SetActive(newState != GameManager.State.Menu);
        gamePanel.SetActive(newState == GameManager.State.Game);

        if (newState == GameManager.State.Launch)
        {
            gamePanel.SetActive(false);
            savedPanel.SetActive(true);

            winnerImage.color = GameManager.Instance.Winner.Color;
        }
        
        if (newState == GameManager.State.GameOver)
        {
            gamePanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }

    void Update()
    {
        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            if (paused)
            {
                CancelPause();
            }
            else
            {
                Pause();
            }
        }

        if (GameManager.Instance.CurrentState == GameManager.State.Game)
        {
            var currentTime = Mathf.Round(GameManager.Instance.CurrentTime);
            var min = (int) currentTime / 60;
            var sec = "" + currentTime % 60;
            sec = sec.Length > 1 ? sec : $"0{sec}";
            currentTimeText.text = $"{min}:{sec}";

            var rocket = Rocket.Instance;
            currentRocketValue.text = $"{rocket.CurrentValue}/{rocket.MaxValue}";
        }
    }
}
