using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(CinemachineTargetGroup))]
public class TargetGroupPlayerAssigner : GameManagerStateListener
{
    [SerializeField]
    float defaultRadius = default;

    [SerializeField]
    CinemachineTargetGroup targetGroup = default;

    private static TargetGroupPlayerAssigner instance;
    public static TargetGroupPlayerAssigner Instance => instance;

    protected void Awake()
    {
        instance = this;
    }
    
    protected override void Start()
    {
        LocalMultiplayerManager.Instance.OnPlayerJoined += OnPlayerJoined;
        targetGroup.AddMember(Rocket.Instance.transform, 1, defaultRadius);
    }

    protected override void OnDestroy()
    {
        if (LocalMultiplayerManager.Instance != null)
        {
            LocalMultiplayerManager.Instance.OnPlayerJoined -= OnPlayerJoined;
        }
    }

    protected override void OnGameManagerStateChange(GameManager.State newState)
    {
        targetGroup.DoUpdate();
    }

    void OnPlayerJoined(PlayerInfo playerInfo)
    {
        targetGroup.AddMember(playerInfo.transform, 1, defaultRadius);
    }

    public void AddPlayer(Transform newTransform)
    {
        targetGroup.AddMember(newTransform, 1, defaultRadius);
    }

    public void RemovePlayer(Transform newTransform)
    {
        targetGroup.RemoveMember(newTransform);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }
#endif
}
