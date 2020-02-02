using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;


public class RocketFlyEventController : MonoBehaviour
{
    enum RocketFlyState
    {
        WAITING_FOR_END,
        WAITING,
        PLAYER_JUMPING,
        ROCKET_FLYING
    }

    [SerializeField]
    Transform target;
    [SerializeField]
    GameObject rocket;
    [SerializeField]
    PlayableDirector director;

    [SerializeField]
    RocketFlyState currentState = RocketFlyState.WAITING_FOR_END;

    [Header("Player jump")]
    [SerializeField]
    float jumpLerp;
    [SerializeField]
    float jumpScale;

    [Header("Rocket flyaway")]
    [SerializeField]
    float rocketSpeed;
    [SerializeField]
    Vector3 eulerRotation;
    [SerializeField]
    float throwAwayForce;
    [SerializeField]
    GameObject onGroundParticles;
    [SerializeField]
    GameObject flyingParticles;
    [SerializeField]
    CinemachineVirtualCamera flyAwayCamera;

    Transform winner;
    Rigidbody[] otherObjects;
    Vector3 rocketDirection;
    Vector3 winnerOrigin;

    public void PlayerJumpToRocket()
    {
        var w = GameManager.Instance.Winner;
        if (w == null)
        {
            Debug.LogError("Winner is null!");
            return;
        }
        winner = w.GetComponent<Transform>();

        var rb = winner.GetComponent<Rigidbody>();
        var collider = winner.GetComponent<Collider>();

        rb.isKinematic = true;
        rb.useGravity = false;
        collider.enabled = false;
        winnerOrigin = rb.position;

        currentState = RocketFlyState.PLAYER_JUMPING;
    }

    public void RocketFlyAway()
    {
        var players = FindObjectsOfType<PlayerInfo>()
            .Select(pi => pi.GetComponent<Transform>())
            .Where(tr => tr != winner);
        var boxes = FindObjectsOfType<Box>()
            .Select(b => b.GetComponent<Transform>());

        otherObjects = players.Union(boxes)
            .Select(o => o.GetComponent<Rigidbody>())
            .ToArray();

        rocketDirection = Vector3.up;

        onGroundParticles.SetActive(false);
        flyingParticles.SetActive(true);
        flyAwayCamera.enabled = true;

        currentState = RocketFlyState.ROCKET_FLYING;
    }

    public void PlayerDissapear()
    {
        winner.gameObject.SetActive(false);
    }

    void Update()
    {
        switch(currentState)
        {
            case RocketFlyState.WAITING_FOR_END:
                if (GameManager.Instance.CurrentState == GameManager.State.Launch)
                {
                    director.Play();
                    currentState = RocketFlyState.WAITING;
                }

                break;
            case RocketFlyState.PLAYER_JUMPING:
                PlayerJumpingState();
                break;
            case RocketFlyState.ROCKET_FLYING:
                foreach (var o in otherObjects)
                {
                    o.AddExplosionForce(throwAwayForce, Vector3.zero, 40f);
                }

                RocketFlyingState();
                break;
        }
    }

    void PlayerJumpingState()
    {
        winner.position = Vector3.SlerpUnclamped(winnerOrigin, target.position, jumpLerp);
        winner.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, jumpScale);
    }

    void RocketFlyingState()
    {
        rocket.transform.position += (rocketDirection * rocketSpeed * Time.deltaTime);
    }
}
