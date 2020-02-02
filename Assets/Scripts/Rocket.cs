using System.Collections.Generic;
using UnityEngine;

public class Rocket : StaticAccess<Rocket>
{
    [SerializeField] private int maxValue = default;
    [SerializeField] private int currentValue = default;

    [SerializeField] private GameObject ghost = default;
    [SerializeField] private GameObject real = default;

    [SerializeField] private Renderer[] ghostRenderers;

    [SerializeField] private float fillSpeed = 1f;

    MaterialPropertyBlock propertyBlock;

    float targetFill;
    float currentFill;
    
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

        targetFill = (float)currentValue / (float)maxValue;

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
        targetFill = (float)currentValue / (float)maxValue;
        return box;
    }

    void LaunchWith(PlayerInteraction player)
    {
        isLaunching = true;
        GameManager.Instance.LaunchRocket(player.GetComponent<PlayerInfo>());
    }

    void SwapRockets()
    {
        ghost.SetActive(false);
        real.SetActive(true);
    }

    void Start()
    {
        propertyBlock = new MaterialPropertyBlock();
    }

    void Update()
    {
        currentFill = Mathf.Lerp(currentFill, targetFill, fillSpeed * Time.deltaTime);

        propertyBlock.SetFloat("_FillAmount", currentFill);

        foreach (var r in ghostRenderers)
        {
            r.SetPropertyBlock(propertyBlock);
        }
    }
}