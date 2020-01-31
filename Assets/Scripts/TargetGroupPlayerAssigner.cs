using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(CinemachineTargetGroup))]
public class TargetGroupPlayerAssigner : MonoBehaviour
{
    [SerializeField]
    float defaultRadius;

    [SerializeField]
    CinemachineTargetGroup targetGroup;

    LocalMultiplayerManager multiplayerManager;

    void Start()
    {
        multiplayerManager = FindObjectOfType<LocalMultiplayerManager>();
        multiplayerManager.onPlayerJoined += PlayerJoined;
    }

    void PlayerJoined(Transform tr)
    {
        targetGroup.AddMember(tr, 1, defaultRadius);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }
#endif
}
