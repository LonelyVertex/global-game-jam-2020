using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{

    [Header("Area")]
    [SerializeField] private float areaWidth;
    [SerializeField] private float areaHeight;
    [SerializeField] private float innerAreaWidth;
    [SerializeField] private float innerAreaHeight;

    private static SpawnManager instance;
    private Vector3 position;

    public static SpawnManager Instance => instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        position = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        var transformPosition = transform.position;
        Gizmos.DrawWireCube(transformPosition, new Vector3(areaWidth, 1, areaHeight));
        Gizmos.DrawWireCube(transformPosition, new Vector3(innerAreaWidth, 1, innerAreaHeight));
    }

    public Vector3 RandomSpawnPosition()
    {
        var center = position.x;
        var x = RandomLinear(position.x, 0, areaWidth);
        var anyZ = x < center - innerAreaWidth / 2 || x > center + innerAreaWidth / 2;
        var z = RandomLinear(position.z, anyZ ? 0 : innerAreaHeight, areaHeight);
        return new Vector3(x, position.y, z);
    }

    static float RandomLinear(float center, float innerSize, float outerSize)
    {
        var a1 = center - outerSize / 2;
        var a2 = center - innerSize / 2;
        var a3 = center + innerSize / 2;
        var a4 = center + outerSize / 2;

        var x = Random.Range(0f, 1f);

        return x <= 0.5
            ? 2 * x * (a2 - a1) + a1
            : (2 * x - 1) * (a4 - a3) + a3;
    }
}