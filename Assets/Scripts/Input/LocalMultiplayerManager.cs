using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


[RequireComponent(typeof(PlayerInputManager))]
public class LocalMultiplayerManager : MonoBehaviour
{
    const string KEYBOARD_SCHEME = "Keyboard";
    const string KEYBOARD_ALT_SCHEME = "Keyboard Alt";
    const string KEYBOARD_NUM_SCHEME = "Keyboard Num";
    const string GAMEPAD_SCHEME = "Gamepad";

    [SerializeField]
    PlayerInputManager inputManager;

    HashSet<KeyValuePair<InputDevice, string>> pairedDevices = new HashSet<KeyValuePair<InputDevice, string>>();


    void Update()
    {
        var keyboard = Keyboard.current;
        if (
            keyboard[Key.LeftShift].wasPressedThisFrame &&
            !IsDevicePaired(keyboard, KEYBOARD_SCHEME)
        ) {
            JoinPlayer(keyboard, KEYBOARD_SCHEME);
        }

        if (
            keyboard[Key.RightAlt].wasPressedThisFrame &&
            !IsDevicePaired(keyboard, KEYBOARD_ALT_SCHEME)
        ) {
            JoinPlayer(keyboard, KEYBOARD_ALT_SCHEME);
        }

        if (
            keyboard[Key.Digit0].wasPressedThisFrame &&
            !IsDevicePaired(keyboard, KEYBOARD_NUM_SCHEME)
        ) {
            JoinPlayer(keyboard, KEYBOARD_NUM_SCHEME);
        }

        var gamepads = Gamepad.all;
        foreach (var g in gamepads)
        {
            if (
                g[GamepadButton.South].wasPressedThisFrame &&
                !IsDevicePaired(g, GAMEPAD_SCHEME)
            ) {
                JoinPlayer(g, GAMEPAD_SCHEME);
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
