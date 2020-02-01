using UnityEngine;
using UnityEngine.InputSystem;

public class InputPool : MonoBehaviour
{
    [SerializeField] private ControlsIndicator keyboardArrows;
    [SerializeField] private ControlsIndicator keyboardWSAD;
    [SerializeField] private ControlsIndicator gamepad;

    private int gamepadCount;

    private int currentGamepads;

    void Start()
    {
        gamepadCount = Gamepad.all.Count;
        HideGamePad();
        LocalMultiplayerManager.Instance.OnPlayerJoined += OnPlayerJoined;
        
        keyboardArrows.SetIndicating(true);
        keyboardWSAD.SetIndicating(true);
        gamepad.SetIndicating(true);
    }

    void OnDestroy()
    {
        if (LocalMultiplayerManager.Instance != null)
        {
            LocalMultiplayerManager.Instance.OnPlayerJoined -= OnPlayerJoined;
        }
    }
    
    void OnPlayerJoined(PlayerInfo playerInfo)
    {
        switch (playerInfo.ControlScheme)
        {
            case ControlScheme.KEYBOARD:
                keyboardWSAD.gameObject.SetActive(false);
                break;
            case ControlScheme.KEYBOARD_ALT:
                keyboardArrows.gameObject.SetActive(false);
                break;
            case ControlScheme.GAMEPAD:
                currentGamepads++;
                HideGamePad();
                break;
        }
    }

    void HideGamePad()
    {
        if (currentGamepads == gamepadCount)
        {
            gamepad.gameObject.SetActive(false);
        }
    }
}