using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Terrain
{
    [SerializeField] List<WoodLog> woodlogPrefabList;
    [SerializeField] float minwoodlogSpawnInterval;
    [SerializeField] float maxwoodlogSpawnInterval;

    float timer;
    Vector3 woodlogSpawnPosition;
    Quaternion woodlogRotation;

    private WoodLog currentWoodLog;

    private void Start()
    {
        if (Random.value > 0.5f)
        {
            woodlogSpawnPosition = new Vector3(horizontalSize / 2 + 7, 0, this.transform.position.z);
            woodlogRotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            woodlogSpawnPosition = new Vector3(-(horizontalSize / 2 + 7), 0, this.transform.position.z);
            woodlogRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Update()
    {
        if (timer <= 0 && currentWoodLog == null)
        {
            var randomIndex = Random.Range(0, woodlogPrefabList.Count);
            timer = Random.Range(minwoodlogSpawnInterval, maxwoodlogSpawnInterval);
            var prefab = woodlogPrefabList[randomIndex];
            currentWoodLog = Instantiate(prefab, woodlogSpawnPosition, woodlogRotation);

            currentWoodLog.SetDistanceLimit(horizontalSize + 15);
        }

        if (currentWoodLog != null && Vector3.Distance(currentWoodLog.transform.position, woodlogSpawnPosition) > currentWoodLog.distanceLimit)
        {
            Destroy(currentWoodLog.gameObject);
            currentWoodLog = null;
        }

        timer -= Time.deltaTime;
    }
}
