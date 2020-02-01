using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour
{
    [SerializeField] private float aliveTime = default;
    [SerializeField] private int value = default;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;

    public int Value => value;

    private bool canBePicked = true;
    public bool CanBePicked => canBePicked;

    private Collider playerCollider;

    void Start()
    {
        Invoke(nameof(Despawn), aliveTime);
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
