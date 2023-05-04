using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;


    [Header("Timer Settings")]
    public float gametime;
    public float lasttime;
    public bool countUp;

    void setText()
    {
        int min = Mathf.FloorToInt(gametime / 60);
        int sec = Mathf.FloorToInt(gametime % 60);
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        gametime = countUp ? gametime += Time.deltaTime : gametime = lasttime;
        lasttime = gametime;
        setText();
    }
}
