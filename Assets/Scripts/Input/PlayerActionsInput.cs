using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerActionsInput : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovement = default;
    [SerializeField]
    PlayerInput playerInput = default;

    Vector2 controllerInput;


    void OnMovement(InputValue val)
    {
        controllerInput = val.Get<Vector2>();


        playerMovement.SetInput(controllerInput);
    }
    
#if UNITY_EDITOR
    void OnValidate()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
    }
#endif
}
