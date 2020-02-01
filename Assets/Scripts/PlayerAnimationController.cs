using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Animator animator;

    int velocityId;

    void Start()
    {
        velocityId = Animator.StringToHash("Velocity");
    }

    void Update()
    {
        animator.SetFloat(velocityId, rb.velocity.magnitude);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
#endif
}
