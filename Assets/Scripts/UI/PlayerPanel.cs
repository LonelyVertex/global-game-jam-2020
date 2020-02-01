using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image colorIndicator = default;
    [SerializeField] private GameObject readyIndicator = default;
    [SerializeField] private ControlsIndicator keyboard = default;
    [SerializeField] private ControlsIndicator gamepad = default;

    [Header("Sprites")]
    [SerializeField] private Sprite keyboardSprite = default;
    [SerializeField] private Sprite keyboardAltSprite = default;
    [SerializeField] private Sprite keyboardNumSprite = default;
    
    private PlayerInfo playerInfo;
    private ControlsIndicator currentIndication;


    public void SetPlayerInfo(PlayerInfo newPlayerInfo)
    {
        playerInfo = newPlayerInfo;
        colorIndicator.color = playerInfo.Color;
        ShowControlScheme(newPlayerInfo.ControlScheme);
    }

    void Update()
    {
        readyIndicator.SetActive(playerInfo.Ready);

        if (currentIndication)
        {
            currentIndication.SetIndicating(!playerInfo.Ready);
        }
    }

    void ShowControlScheme(string controlScheme)
    {
        switch (controlScheme)
        {
            case ControlScheme.KEYBOARD:
                keyboard.GetComponent<Image>().sprite = keyboardSprite;
                keyboard.gameObject.SetActive(true);
                currentIndication = keyboard;
                break;
            
            case ControlScheme.KEYBOARD_ALT:
                keyboard.GetComponent<Image>().sprite = keyboardAltSprite;
                keyboard.gameObject.SetActive(true);
                currentIndication = keyboard;
                break;
            
            case ControlScheme.KEYBOARD_NUM:
                keyboard.GetComponent<Image>().sprite = keyboardNumSprite;
                keyboard.gameObject.SetActive(true);
                currentIndication = keyboard;
                break;
            
            case ControlScheme.GAMEPAD:
                gamepad.gameObject.SetActive(true);
                currentIndication = gamepad;
                break;
        }
    }
}
