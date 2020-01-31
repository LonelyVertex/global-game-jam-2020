using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerActionsInput : MonoBehaviour
{
    [SerializeField]
    PlayerInput input = default;
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

    void OnAction()
    {
        Debug.Log("Action!");
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        input = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }
#endif
}
