using UnityEngine;

public class ControlsIndicator : MonoBehaviour
{
    [SerializeField] private Animator animator = default;
    private static readonly int Fading = Animator.StringToHash("Fading");

    public void SetIndicating(bool indicating)
    {
        animator.SetBool(Fading, indicating);
    }
}
