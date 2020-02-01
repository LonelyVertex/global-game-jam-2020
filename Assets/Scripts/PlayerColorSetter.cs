using UnityEngine;


[RequireComponent(typeof(PlayerInfo))]
public class PlayerColorSetter : MonoBehaviour
{
    [System.Serializable]
    struct MaterialPropertyInfo
    {
        public Renderer renderer;
        public int index;
    }

    readonly int PLAYER_COLOR_ID = Shader.PropertyToID("_BaseColor");

    [SerializeField]
    MaterialPropertyInfo[] meshRenderers;

    [SerializeField]
    PlayerInfo playerInfo;


    void Start()
    {
        var color = playerInfo.Color;


        foreach (var r in meshRenderers)
        {
            var mp = new MaterialPropertyBlock();
            r.renderer.GetPropertyBlock(mp, r.index);

            mp.SetColor(PLAYER_COLOR_ID, color);

            r.renderer.SetPropertyBlock(mp, r.index);
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }
#endif
}
