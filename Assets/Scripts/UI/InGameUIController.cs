public class InGameUIController : GameManagerStateListener
{
    protected override void OnGameManagerStateChange(GameManager.State newState)
    {
        gameObject.SetActive(newState == GameManager.State.Game);
    }
}
