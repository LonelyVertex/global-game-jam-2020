using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private Color color;
    private bool ready;
    private string controlScheme;

    public Color Color => color;
    public bool Ready => ready;
    public string ControlScheme => controlScheme;
    
    void Start()
    {
        color = ColorPicker.Instance.NextColor();
        transform.position = SpawnManager.Instance.RandomSpawnPosition();
        controlScheme = GetComponent<PlayerInput>().currentControlScheme;
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
