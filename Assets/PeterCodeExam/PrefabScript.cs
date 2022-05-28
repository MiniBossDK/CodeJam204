using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PrefabScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText, alarmWarning;
    [SerializeField] private Button stopButton;

    //Code borrowed from WhipEffect script, and modified to suit the intended functionality. 
    float thresh = 2f;
    private Vector3 accelInfo;

    void Start()
    {
        stopButton.onClick.AddListener(StopAlarm);
        SensorManager.Instance.OnAcceleration += AccelerationCheck;

        if (PlayerPrefs.GetString("alarmMessage") != null)
        {
            alarmWarning.text = PlayerPrefs.GetString("alarmMessage");
        }
        else if (PlayerPrefs.GetString("alarmMessage") == null)
        {
            alarmWarning.gameObject.SetActive(false);
        }
    }

    //The variables and the assignment of value to timerText is taken from the tutorial: https://www.youtube.com/watch?v=zHAsc5H0j2c&ab_channel=MetalStormGames
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
    }

    //Code inspired from WhipEffect script, but with fewer conditions
    void AccelerationCheck(Vector3 vector)
    {
    accelInfo = vector;
            if (accelInfo.magnitude > thresh)
            {
                StopAlarm();
            }
    }
}
