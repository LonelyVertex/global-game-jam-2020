using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = default;
    [SerializeField]
    Rigidbody rb = default;
    [SerializeField]
    Transform animatorWrapper = default;

    Vector2 input;

    public void SetInput(Vector2 i)
        => input = i;

    void FixedUpdate()
    {
        if (GameManager.Instance.CurrentState != GameManager.State.Game)
            return;

        Vector3 movement = new Vector3(input.x, 0, input.y).normalized;

        rb.AddForce(movement * speed);

        if (rb.velocity.magnitude > 0.1f)
        {
            animatorWrapper.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Respawn();
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
