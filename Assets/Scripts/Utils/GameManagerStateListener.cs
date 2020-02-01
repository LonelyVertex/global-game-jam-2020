using UnityEngine;

public abstract class GameManagerStateListener : MonoBehaviour
{
    protected virtual void Start()
    {
        GameManager.Instance.OnStateChange += OnGameManagerStateChange;
        OnGameManagerStateChange(GameManager.Instance.CurrentState);
    }

    protected virtual void OnDestroy()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.OnStateChange -= OnGameManagerStateChange;
        }
    }

    protected abstract void OnGameManagerStateChange(GameManager.State newState);
}
