using UnityEngine;


[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimationController : MonoBehaviour
{
    readonly int velocityId = Animator.StringToHash("Velocity");

    [SerializeField]
    PlayerMovement playerMovement;
    [SerializeField]
    Animator animator;


    void Update()
    {
        animator.SetFloat(velocityId, playerMovement.Velocity.magnitude);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();
    }
#endif
}
