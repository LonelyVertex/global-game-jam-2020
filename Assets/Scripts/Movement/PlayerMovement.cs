using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = default;
    [SerializeField]
    Rigidbody rb = default;

    Vector2 input;

    public void SetInput(Vector2 i)
        => input = i;

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(input.x, 0, input.y).normalized;

        rb.AddForce(movement * speed);
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
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        rb = GetComponent<Rigidbody>();    
    }
#endif
}
