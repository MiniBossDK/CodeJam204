using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : SingletonPattern<ButtonController>
{
    public Button button;
    public float savedLatitude;
    public float savedLongitude;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        savedLatitude = Input.location.lastData.latitude;
        savedLongitude = Input.location.lastData.longitude;
    }
}
