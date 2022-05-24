using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PrefabScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button stopButton;

    //Timer skal kunne stoppes både ved at klikke på en knap, men også ved at bruge accelerometeret. Undersøg hvordan man kan indarbejde WhipEffect principer i dette script. 
    //Code borrowed from WhipEffect script, and modified to suit the intended functionality. 
    float thresh = 2f;
    private Vector3 accelInfo;


    private Transform parentCanvas;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        stopButton.onClick.AddListener(StopAlarm);
        SensorManager.Instance.OnAcceleration += AccelerationCheck;
    }

    // Update is called once per frame
    void Update()
    {
        int hour = DateTime.Now.Hour;
        int minute = DateTime.Now.Minute;
        int second = DateTime.Now.Second;

        timerText.text = $"{hour:D2}:{minute:D2}:{second:D2}";
    }


    private void StopAlarm()
    {
        Destroy(gameObject);
        Debug.Log("Test Stop");
    }

    void AccelerationCheck(Vector3 vector)
    {
    accelInfo = vector;
            if (accelInfo.magnitude > thresh)
            {
                StopAlarm();
            }
    }
}
