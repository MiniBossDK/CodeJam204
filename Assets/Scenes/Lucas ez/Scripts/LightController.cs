using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    FlashLightManager FLM;
    GPSManager GPS;
    public bool isDone = false;

    private void Start()
    {
        GPS = GPSManager.Instance;
        FLM = FlashLightManager.Instance;
    }

    private void Update()
    {
        LocationChecker();
    }

    // m�ske bruge switch? men ville jeg s� ik sku bruge en switch for b�de latitude?
    private void LocationChecker()
    {
        if (!isDone && GPS.latitude > GPS.savedLatitude || GPS.latitude < GPS.savedLatitude)
        {
            FLM.FL_Start();
            isDone = true;
        }
        //else if (!isDone && GPS.longitude > GPS.savedLongitude || GPS.longitude < GPS.savedLongitude)
        //{
        //    FLM.FL_Start();
        //    isDone = true;
        //}
        else
        {
            FLM.FL_Stop();
        }
    }
        
}
