using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Vector3 Velocity => velocity;

    [SerializeField] float speed = default;
    [SerializeField] float respawnDelay = default;
    [SerializeField] Rigidbody rb = default;
    [SerializeField] Transform animatorWrapper = default;

    [SerializeField] float acceleration = 0.2f;

    Quaternion targetRotation;

    Vector2 input;
    Vector3 speedSmoothing;
    Vector3 currentSpeed;

    Vector3 prevMovement;

    Vector3 velocity;

    public void SetInput(Vector2 i)
        => input = i;

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameManager.State.Game)
            return;

        var prevPos = rb.position;

        Vector3 movement = new Vector3(input.x, 0, input.y).normalized;

        var targetSpeed = movement * speed;
        currentSpeed = Vector3.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothing, acceleration);

        rb.MovePosition(prevPos + (currentSpeed * Time.fixedDeltaTime));

        velocity = rb.position - prevPos;
        
        var rotationVelocity = new Vector3(velocity.x, 0, velocity.z);
        if (rotationVelocity.magnitude > 0.1f)
        {
            targetRotation = Quaternion.LookRotation(rotationVelocity);
            animatorWrapper.rotation = Quaternion.RotateTowards(animatorWrapper.rotation, targetRotation, 15f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Invoke(nameof(Respawn), respawnDelay);
        }
    }

    void Respawn()
    {
        SendMessage("OnDeath");
        rb.velocity = Vector3.zero;
        transform.position = SpawnManager.Instance.RandomSpawnPosition();
        animatorWrapper.rotation = Quaternion.identity;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        rb = GetComponent<Rigidbody>();    
    }
#endif
}
