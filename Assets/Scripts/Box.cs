﻿using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private float aliveTime;

    public int Value => 1;

    private bool canBePicked = true;

    public bool CanBePicked => canBePicked;


    void Start()
    {
        Invoke(nameof(Despawn), aliveTime);
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

    public void OnPick(Transform parent)
    {
        CancelInvoke(nameof(Despawn));
        gameObject.SetActive(true);
        canBePicked = false;
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
    }

    public void OnDrop()
    {
        Invoke(nameof(Despawn), aliveTime);
        transform.parent = null;
        canBePicked = true;
    }

    public void OnEnterRocket()
    {
        CancelInvoke(nameof(Despawn));
        gameObject.SetActive(false);
        transform.parent = null;
        canBePicked = false;
    }
}
