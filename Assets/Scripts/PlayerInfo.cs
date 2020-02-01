using UnityEngine;


public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private Color color;
    private bool ready;

    public Color Color => color;
    public bool Ready => ready;
    
    void Start()
    {
        color = ColorPicker.Instance.NextColor();
        transform.position = SpawnManager.Instance.RandomSpawnPosition();
        LocalMultiplayerManager.Instance.RegisterPlayer(this);
    }

    void OnAction()
    {
        if (GameManager.Instance.CurrentState == GameManager.State.Menu)
        {
            ready = true;
        }
    }
}
