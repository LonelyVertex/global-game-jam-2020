using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerActionsInput : GameManagerStateListener
{
    [SerializeField]
    PlayerInput input = default;
    [SerializeField]
    PlayerMovement playerMovement = default;

    Vector2 controllerInput;
    private bool controlsEnabled = false;

    void Update()
    {
        if (controlsEnabled)
        {
            playerMovement.SetInput(controllerInput);
        }
    }

    void OnMovement(InputValue val)
    {
        controllerInput = val.Get<Vector2>();
    }

    void OnAction()
    {
    }
    
    protected override void OnGameManagerStateChange(GameManager.State newState)
    {
        controlsEnabled = newState == GameManager.State.Game;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        input = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }
#endif
}
