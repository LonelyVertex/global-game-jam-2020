using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private Image colorIndicator = default;
    [SerializeField] private GameObject readyIndicator = default;
    private PlayerInfo playerInfo;

    public void SetPlayerInfo(PlayerInfo newPlayerInfo)
    {
        playerInfo = newPlayerInfo;
        colorIndicator.color = playerInfo.Color;
    }

    void Update()
    {
        readyIndicator.SetActive(playerInfo.Ready);
    }
}
