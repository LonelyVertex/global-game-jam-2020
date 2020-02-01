using System.Linq;
using UnityEngine;

public class ColorPicker : StaticAccess<ColorPicker>
{
    [SerializeField] private Color[] colors = default;

    private int currentColor = -1;

    void Start()
    {
        colors = colors.OrderBy(x => Random.value).ToArray();
    }

    public Color NextColor()
    {
        if (colors == null) return Color.white;

        currentColor = (currentColor + 1) % colors.Length;
        return colors[currentColor];
    }
}
