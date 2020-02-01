using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = default;
    [SerializeField] float respawnDelay = default;
    [SerializeField] Rigidbody rb = default;
    [SerializeField] Transform animatorWrapper = default;

    Quaternion targetRotation;

    Vector2 input;

    public void SetInput(Vector2 i)
        => input = i;

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameManager.State.Game)
            return;

        Vector3 movement = new Vector3(input.x, 0, input.y).normalized;

        rb.AddForce(movement * speed);

        var rotationVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (rotationVelocity.magnitude > 0.1f)
        {
            targetRotation = Quaternion.LookRotation(rotationVelocity);
            animatorWrapper.rotation = Quaternion.RotateTowards(animatorWrapper.rotation, targetRotation, 10f);
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
