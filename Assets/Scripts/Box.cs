using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private float aliveTime;

    void Start()
    {
        Invoke(nameof(Despawn), aliveTime);
    }

    void Despawn()
    {
        Destroy(gameObject);
    }
}