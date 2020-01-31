public class MenuController : GameManagerStateListener
{
    protected override void OnGameManagerStateChange(GameManager.State newState)
    {
        gameObject.SetActive(newState == GameManager.State.Menu);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
