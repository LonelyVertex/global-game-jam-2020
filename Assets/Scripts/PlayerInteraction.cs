using System.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float pickRadius;
    [SerializeField] private float rocketDistance;
    private Box currentBox;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickRadius);
    }

    void OnAction()
    {
        var isNearRocket = IsNearRocket();
        var hasBox = currentBox == null;

        if (hasBox)
        {
            if (isNearRocket)
            {
                PopBoxFromRocket();
            }
            else
            {
                TryPickUpBox();
            }
        }
        else
        {
            if (isNearRocket)
            {
                PushBoxToRocket();
            }
            else
            {
                DropBox();
            }
        }
    }

    void TryPickUpBox()
    {
        float Distance(Box b) => Vector3.Distance(b.transform.position, transform.position);

        var box = FindObjectsOfType<Box>()
            .Where(b => b.CanBePicked)
            .OrderBy(Distance)
            .First();

        if (box == null || Distance(box) > pickRadius) return;

        currentBox = box;
        box.OnPick(transform);
    }

    void DropBox()
    {
        currentBox.OnDrop();
        currentBox = null;
    }

    void PushBoxToRocket()
    {
        if (Rocket.Instance.PushBox(currentBox, this))
        {
            currentBox = null;
        }
    }

    void PopBoxFromRocket()
    {
        currentBox = Rocket.Instance.PopBox();
        if (currentBox)
        {
            currentBox.OnPick(transform);
        }
    }

    bool IsNearRocket()
    {
        return rocketDistance >= Vector3.Distance(Rocket.Instance.transform.position, transform.position);
    }
}