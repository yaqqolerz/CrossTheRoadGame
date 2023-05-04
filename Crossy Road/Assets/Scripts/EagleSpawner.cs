using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleSpawner : MonoBehaviour
{

    [SerializeField] Eagle eagle;
    
    [SerializeField] Duck duck;

    [SerializeField] float initialTimer = 10;

    float timer;
    // Start is called before the first frame update
    private void Start()
    {
        timer = initialTimer;
        eagle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (timer <= 0 && eagle.gameObject.activeInHierarchy == false)
        {
            duck.setNotMoveable(true);
            eagle.gameObject.SetActive(true);
            eagle.transform.position = duck.transform.position + new Vector3(0, 0, 13);
            
        }

        timer -= Time.deltaTime;
    }

    public void resetTimer()
    {
        timer = initialTimer;
    }
}
