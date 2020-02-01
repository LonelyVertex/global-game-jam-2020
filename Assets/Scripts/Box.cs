using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour
{
    public enum Size
    {
        Small = 0,
        Medium = 1,
        Large = 2
    }
    
    [SerializeField] private float aliveTime = default;
    [SerializeField] private Rigidbody rb = default;
    [SerializeField] private Collider col = default;

    [Header("Box Settings")]
    [SerializeField] private float smallBoxScale = default;
    [SerializeField] private int smallBoxValue = default;
    [SerializeField] private float mediumBoxScale = default;
    [SerializeField] private int mediumBoxValue = default;
    [SerializeField] private float largeBoxScale = default;
    [SerializeField] private int largeBoxValue = default;

    private int value;
    public int Value => value;

    private bool canBePicked = true;
    public bool CanBePicked => canBePicked;

    private Collider playerCollider;

    void Start()
    {
        RandmizeSize();
        Invoke(nameof(Despawn), aliveTime);
    }
    
    void RandmizeSize()
    {
        var values = Enum.GetValues(typeof(Size));
        var size = (Size) values.GetValue(Random.Range(0, values.Length));
        
        switch (size)
        {
            case Size.Small:
                value = smallBoxValue;
                transform.localScale = new Vector3(smallBoxScale, smallBoxScale, smallBoxScale);
                break;
            case Size.Medium:
                value = mediumBoxValue;
                transform.localScale = new Vector3(mediumBoxScale, mediumBoxScale, mediumBoxScale);
                break;
            case Size.Large:
                value = largeBoxValue;
                transform.localScale = new Vector3(largeBoxScale, largeBoxScale, largeBoxScale);
                break;
        }   
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

    public void OnPick(Transform parent, Collider newPlayerCollider)
    {
        CancelInvoke(nameof(Despawn));
        gameObject.SetActive(true);
        playerCollider = newPlayerCollider;
        canBePicked = false;
        rb.isKinematic = true;
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        Physics.IgnoreCollision(col, playerCollider, true);
    }

    public void OnDrop()
    {
        Invoke(nameof(Despawn), aliveTime);
        canBePicked = true;
        rb.isKinematic = false;
        transform.parent = null;
        Physics.IgnoreCollision(col, playerCollider, false);
    }

    public void OnEnterRocket()
    {
        CancelInvoke(nameof(Despawn));
        gameObject.SetActive(false);
        transform.parent = null;
        canBePicked = false;
    }

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
}
