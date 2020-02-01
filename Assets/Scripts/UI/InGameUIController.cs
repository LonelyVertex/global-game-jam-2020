using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameUIController : GameManagerStateListener
{
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject savedPanel;

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
        if (newState == GameManager.State.Game)
        {
            gameObject.SetActive(true);
            gamePanel.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
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
    }
}
