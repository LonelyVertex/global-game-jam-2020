using System.Collections.Generic;
using UnityEngine;

public class Rocket : StaticAccess<Rocket>
{
    [SerializeField] private int maxValue = default;
    [SerializeField] private int currentValue = default;
    
    private Stack<Box> boxes = new Stack<Box>();
    private bool isLaunching;

    public int MaxValue => maxValue;
    public int CurrentValue => currentValue;

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
        isLaunching = true;
        GameManager.Instance.LaunchRocket(player.GetComponent<PlayerInfo>());
    }
}