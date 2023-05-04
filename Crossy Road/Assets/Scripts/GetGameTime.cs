using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetGameTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentTime;
    [SerializeField] TextMeshProUGUI TimeText;

    private void Start()
    {
        TimeText.text = currentTime.text;
    }

}
