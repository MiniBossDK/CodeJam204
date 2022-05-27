using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    FlashLightManager FLM;
    GPSManager GPS;
    ButtonController BC;
    public bool isDone = false;
    public float threshold = 00.00010f;


    private void Start()
    {
        GPS = GPSManager.Instance;
        FLM = FlashLightManager.Instance;
        BC = ButtonController.Instance;
    }

    private void Update()
    {
        LocationChecker();
    }

    /// <summary>
    /// This method checks whether the current location differs from the saved location by a threshold, and turns on/off the lights accordingly
    /// </summary>
    private void LocationChecker()
    {
        if (!isDone && GPS.latitude > BC.savedLatitude + threshold || !isDone && GPS.latitude < BC.savedLatitude - threshold)
        {
            FLM.FL_Stop();
            isDone = true;
        }
        else if (!isDone && GPS.longitude > BC.savedLongitude + threshold || !isDone && GPS.longitude < BC.savedLongitude - threshold)
        {
            FLM.FL_Stop();
            isDone = true;
        }
        else if (isDone && GPS.latitude == BC.savedLatitude || isDone && GPS.longitude == BC.savedLongitude)
        {
            FLM.FL_Start();
            isDone = false;
        }
    }
        
}
