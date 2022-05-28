using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SetTimerScript : SingletonPattern<SetTimerScript>
{
    //Inpsired from tutorial: https://www.youtube.com/watch?v=zHAsc5H0j2c&ab_channel=MetalStormGames

    [SerializeField] private TMP_InputField hours_Input, minutes_Input, seconds_Input, message_Input;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button button;

    private bool isAlarmActive = false;
    private DateTime alarmTime;

    private bool shouldRepeat;
    [SerializeField] Button repeatButton;
    [SerializeField] private GameObject checkMark;
    private string dateRepeat;
    private bool hasTurnedOn = true;

    [SerializeField] private GameObject timerMessage;


    void Start()
    {
        button.onClick.AddListener(SetAlarm);
        repeatButton.onClick.AddListener(ToggleRepeat);
        DontDestroyOnLoad(this);

        dateRepeat = DateTime.Today.DayOfWeek.ToString();
        Debug.Log(dateRepeat);
    }

    //Line 41 through 53 is taken from tutorial: https://www.youtube.com/watch?v=zHAsc5H0j2c&ab_channel=MetalStormGames
    //
    void Update()
    {
        int hours = DateTime.Now.Hour;
        int minutes = DateTime.Now.Minute;
        int seconds = DateTime.Now.Second;
        timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";

        //This will run the method when the time matches the user's given alarm time. 
        if (isAlarmActive && DateTime.Now > alarmTime)
        {
            TimerIsNow();
            Debug.Log("testA");
        }
        //This will check, if the user has checked the repeat button, and should then activate the coroutine when the time matches the alarm time. 
        else if (shouldRepeat && DateTime.Now >= alarmTime)
        {
            if (hasTurnedOn != true)
            {
                StartCoroutine(RepeatDays());
                Debug.Log("testP");
            }
        }

        //This statement is meant to set the boolean back to false, so the above else if statement can be activated again the following week. 
        if (DateTime.Today.DayOfWeek.ToString() != dateRepeat)
        {
            hasTurnedOn = false;
        }
    }

    //Line 72 through 83 is taken from tutorial: https://www.youtube.com/watch?v=zHAsc5H0j2c&ab_channel=MetalStormGames
    private void SetAlarm()
    {
        TimeSpan ts = TimeSpan.Parse($"{hours_Input.text}:{minutes_Input.text}:{seconds_Input.text}");
        alarmTime = DateTime.Today + ts;

        isAlarmActive = true;

        //If the user inputs a time, which is earlier in the day than the given time, it will then set the alarm for the following day. 
        if (DateTime.Now >= alarmTime)
        {
            alarmTime = alarmTime.AddDays(1);
        }

        //If the user has given a message input, it will then be set as a PlayerPrefs string, which the prefab can then get when it is instantiated. 
        PlayerPrefs.SetString("alarmMessage", message_Input.text);

        Debug.Log(alarmTime);

        //
        /*
        if (int.Parse(hours_Input.text) >= 24)
        {
            //Meant as a way to display a way for the user to know they had given an unavailable input, but not as necessary since TimeSpan gives an error log. 
        }*/

        //This statement will set the hasTurnedOn bool as false, if the user has checked the repeat button. This will allow the conditions in the else if statement to be met and should then start the coroutine.
        //It also updates dateRepeat to reflect the day of the alarm, instead of the assigned value in the Start() method. 
        if (shouldRepeat)
        {
            dateRepeat = alarmTime.DayOfWeek.ToString();
            hasTurnedOn = false;
            Debug.Log(dateRepeat);
        }
    }

    //The instantiating was attempted with different approaches, but ended up with this. 
    //The approach was from multiple sources:
    //https://www.youtube.com/watch?v=m2rS7YebZbY&ab_channel=PkMixture
    //https://stackoverflow.com/questions/60854575/instantiating-a-u-i-object-at-the-position-of-another-object-while-keeping-the-c
    private void TimerIsNow()
    {
        GameObject timerDisplay = Instantiate(timerMessage, transform.position, transform.rotation) as GameObject;
        timerDisplay.transform.SetParent(GameObject.Find("Canvas").transform, false);
        isAlarmActive = false;
    }

    //The IEnumerator needs to check several conditions, before it will run the TimerIsNow method. It will then wait untill the bool IsDaySet() also fulfills its conditions to run again. 
    IEnumerator RepeatDays()
    {
        if (hasTurnedOn != true && DateTime.Now >= alarmTime)
        {
            Debug.Log("test23");
            if (alarmTime != null)
            {
                Debug.Log("tetstC");
                TimerIsNow();
                hasTurnedOn = true;
            }
        }
        yield return new WaitUntil(IsTheDaySet);
    }

    //This method will return a true bool, if the current day matches the dateRepeat, and if the hasTurnedOn bool is false. 
    private bool IsTheDaySet()
    {
        if (DateTime.Now.DayOfWeek.ToString() == dateRepeat && hasTurnedOn != true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //This method will toggle the shouldRepeat bool true or false, by setting it to the opposite of what it currently is. In addition, it will set a checkmark image as either active or inactive, according to the bool's value. 
    private void ToggleRepeat()
    {
        shouldRepeat = !shouldRepeat;
        Debug.Log(shouldRepeat);


        if (shouldRepeat == true)
        {
            checkMark.SetActive(true);
        }
        else
        {
            checkMark.SetActive(false);
        }
    }
}
