using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;

    [SerializeField, Range(0, 10)] float rotationSpeed;

    public int Value { get => value; }
    
    public void Collected()
    {
        GetComponent<Collider>().enabled = false;
        rotationSpeed *= 4;
        this.transform.DOJump(this.transform.position, 1, 1, 0.7f).onComplete = selfDestruct;
    }

    private void selfDestruct()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.Rotate(0,rotationSpeed * Time.deltaTime * 360,0);
    }
}
