using UnityEngine;


public class PlayerStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var manager = FindObjectOfType<LocalMultiplayerManager>();

        manager.PlayerJoined(transform);

        Destroy(this);
    }
}
