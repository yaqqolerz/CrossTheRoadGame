using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField, Range(0, 10)] float speed = 1;

    Vector3 initialPos;

    float distanceLimit = float.MaxValue;

    public void SetDistanceLimit(float distance)
    {
        this.distanceLimit = distance;
    }

    private void Start()
    {
        speed = Random.Range(3, 6);
        initialPos = this.transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(initialPos, this.transform.position) > this.distanceLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
