using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float pickRadius = default;
    [SerializeField] private float rocketDistance = default;
    [SerializeField] private Transform boxAnchor = default;
    [SerializeField] private Collider col = default;
    
    private Box currentBox;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickRadius);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rocketDistance);
    }

    void Update()
    {
        if (currentBox)
        {
            currentBox.isNearRocket = IsNearRocket();
        }
    }

    void OnDeath()
    {
        if (currentBox != null)
        {
            Destroy(currentBox.gameObject);
            currentBox = null;
        }
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
        float Distance(Box b) => Vector3.Distance(b.transform.position, transform.position) - b.transform.localScale.x / 2;

        var box = FindObjectsOfType<Box>()
            .Where(b => b.CanBePicked)
            .OrderBy(Distance)
            .FirstOrDefault();

        if (box == null || Distance(box) > pickRadius) return;

        currentBox = box;
        box.OnPick(boxAnchor, col);
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
            currentBox.OnPick(transform, col);
        }
    }

    bool IsNearRocket()
    {
        return rocketDistance >= Vector3.Distance(Rocket.Instance.transform.position, transform.position);
    }

    private void OnValidate()
    {
        col = GetComponent<Collider>();
    }
}