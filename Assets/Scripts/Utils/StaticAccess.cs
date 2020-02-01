using UnityEngine;

public class StaticAccess<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance => instance;

    protected void Awake()
    {
        instance = FindObjectOfType<T>();
    }
}
