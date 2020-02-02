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

    [Header("Materials")]
    [SerializeField] private Renderer renderer;
    [SerializeField] private int materialIndex;

    [Header("Colors")]
    [SerializeField][ColorUsage(false, true)] private Color colorMin;
    [SerializeField][ColorUsage(false, true)] private Color colorMax;
    [SerializeField][ColorUsage(false, true)] private Color colorUsable;
    [SerializeField][ColorUsage(false, true)] private Color colorUnusable;

    private int value;
    public int Value => value;

    private bool canBePicked = true;
    public bool CanBePicked => canBePicked;

    private Collider playerCollider;
    private MaterialPropertyBlock materialPropertyBlock;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private Vector3 rocketPosition;

    public bool isNearRocket = false;

    bool IsUsable
    {
        get
        {
            var rocket = Rocket.Instance;
            return rocket.CurrentValue + value <= rocket.MaxValue;
        }
    }
    
    void Start()
    {
        RandmizeSize();
        Invoke(nameof(Despawn), aliveTime);

        rocketPosition = Rocket.Instance.transform.position;
        
        materialPropertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(materialPropertyBlock, materialIndex);
        
        UpdateColor();
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

    private void Update()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        var targetColor = isNearRocket ? (IsUsable ? colorUsable : colorUnusable) : colorMax;
        var color = Color.Lerp(targetColor, colorMin, Vector3.Distance(transform.position, rocketPosition) / 17);
        materialPropertyBlock.SetColor(EmissionColor, color);
        renderer.SetPropertyBlock(materialPropertyBlock, materialIndex);
    }

    void Despawn()
    {
        if (GameManager.Instance.CurrentState == GameManager.State.Launch)
            return;

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
