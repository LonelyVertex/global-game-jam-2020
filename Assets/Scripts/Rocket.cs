using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private static Rocket instance;
    public static Rocket Instance => instance;

    [SerializeField] private int maxValue;
    [SerializeField] private int currentValue;
    private Stack<Box> boxes = new Stack<Box>();
    private bool isLaunching;

    private void Awake()
    {
        instance = this;
    }

    public bool PushBox(Box box, PlayerInteraction player)
    {
        if (isLaunching || box.Value + currentValue > maxValue)
        {
            return false;
        }

        currentValue += box.Value;
        boxes.Push(box);
        box.OnEnterRocket();

        if (currentValue == maxValue)
        {
            LaunchWith(player);
        }
        
        return true;
    }

    public Box PopBox()
    {
        if (isLaunching || boxes.Count <= 0) return null;
        var box = boxes.Pop();
        currentValue -= box.Value;
        return box;
    }

    void LaunchWith(PlayerInteraction player)
    {
        // TODO - handle launch
        isLaunching = true;
        Debug.Log("Launching rocket with " + player);
    }
}