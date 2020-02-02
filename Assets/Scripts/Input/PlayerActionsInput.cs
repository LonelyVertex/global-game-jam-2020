using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerActionsInput : MonoBehaviour
{
    [SerializeField]
    PlayerMovement playerMovement = default;

    Vector2 controllerInput;

    void Update()
    {
        playerMovement.SetInput(controllerInput);
    }

    void OnMovement(InputValue val)
    {
        controllerInput = val.Get<Vector2>();
    }
    
#if UNITY_EDITOR
    void OnValidate()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
#endif
}
