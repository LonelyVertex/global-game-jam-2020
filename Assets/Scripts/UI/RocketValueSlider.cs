using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class RocketValueSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Update()
    {
        slider.value = (float) Rocket.Instance.CurrentValue / Rocket.Instance.MaxValue;
    }

    private void OnValidate()
    {
        slider = GetComponent<Slider>();
    }
}