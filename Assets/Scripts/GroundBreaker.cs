using UnityEngine;


public class GroundBreaker : MonoBehaviour
{
    [SerializeField]
    float minSpeed = default;
    [SerializeField]
    float maxSpeed = default;

    Vector3 breakVelocity;


    void Start()
    {
        var pos = transform.position;
        breakVelocity = -(new Vector3(pos.x, 0, pos.z).normalized) * Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.State.Game)
            return;

        transform.position += breakVelocity * Time.deltaTime;
    }
}
