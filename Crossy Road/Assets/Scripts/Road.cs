using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] List<Car> carPrefabList;
    [SerializeField] float minCarSpawnInterval;
    [SerializeField] float maxCarSpawnInterval;

    float timer;
    Vector3 CarspawnPosition;
    Quaternion carRotation;
    bool spawned = false;

    private void Start()
    {
        if (Random.value > 0.5f)
        {
            CarspawnPosition = new Vector3(horizontalSize / 2 + 7, 0, this.transform.position.z);
            carRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            CarspawnPosition = new Vector3(-(horizontalSize / 2 + 7), 0, this.transform.position.z);
            carRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Update()
    {
        if (!spawned && timer <= 0)
        {
            var randomIndex = Random.Range(0, carPrefabList.Count);
            var prefab = carPrefabList[randomIndex];
            var car = Instantiate(prefab, CarspawnPosition, carRotation);
            car.SetDistanceLimit(horizontalSize + 15);
            spawned = true;
            return;
        }

        if (spawned && GameObject.FindGameObjectWithTag("Car") == null)
        {
            spawned = false;
            timer = Random.Range(minCarSpawnInterval, maxCarSpawnInterval);
        }

        timer -= Time.deltaTime;
    }
}
