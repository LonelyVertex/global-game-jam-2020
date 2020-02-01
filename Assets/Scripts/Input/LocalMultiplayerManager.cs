using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(PlayerInputManager))]
public class LocalMultiplayerManager : StaticAccess<LocalMultiplayerManager>
{
    public delegate void PlayerJoined(PlayerInfo playerInfo);
    public event PlayerJoined OnPlayerJoined;

    [SerializeField]
    PlayerInputManager inputManager;

    HashSet<KeyValuePair<InputDevice, string>> pairedDevices = new HashSet<KeyValuePair<InputDevice, string>>();
    
    public void RegisterPlayer(PlayerInfo playerInfo)
    {
        OnPlayerJoined?.Invoke(playerInfo);
    }

    void Update()
    {
        if (!GameManager.Instance.PlayersCanJoin) return;
        
        var keyboard = Keyboard.current;
        if (
            keyboard[Key.LeftShift].wasPressedThisFrame &&
            !IsDevicePaired(keyboard, ControlScheme.KEYBOARD)
        ) {
            JoinPlayer(keyboard, ControlScheme.KEYBOARD);
        }

        if (
            keyboard[Key.RightAlt].wasPressedThisFrame &&
            !IsDevicePaired(keyboard, ControlScheme.KEYBOARD_ALT)
        ) {
            JoinPlayer(keyboard, ControlScheme.KEYBOARD_ALT);
        }

        if (
            keyboard[Key.Digit0].wasPressedThisFrame &&
            !IsDevicePaired(keyboard, ControlScheme.KEYBOARD_NUM)
        ) {
            JoinPlayer(keyboard, ControlScheme.KEYBOARD_NUM);
        }

        var gamepads = Gamepad.all;
        foreach (var g in gamepads)
        {
            if (
                g[GamepadButton.South].wasPressedThisFrame &&
                !IsDevicePaired(g, ControlScheme.GAMEPAD)
            ) {
                JoinPlayer(g, ControlScheme.GAMEPAD);
            }
        }
    }

    bool IsDevicePaired(InputDevice inputDevice, string controlScheme)
    {
        return pairedDevices.Contains(new KeyValuePair<InputDevice, string>(inputDevice, controlScheme));
    }

    void JoinPlayer(InputDevice inputDevice, string controlScheme)
    {
        pairedDevices.Add(new KeyValuePair<InputDevice, string>(inputDevice, controlScheme));

        inputManager.JoinPlayer(
            inputManager.playerCount,
            -1,
            controlScheme,
            inputDevice
        );
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        inputManager = GetComponent<PlayerInputManager>();
    }
#endif
}
