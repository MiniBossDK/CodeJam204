using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    FlashLightManager FLM;
    GPSManager GPS;
    ButtonController BC;
    public bool isDone = false;
    public float threshold;

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

    // m�ske bruge switch? men ville jeg s� ik sku bruge en switch for b�de latitude?
    private void LocationChecker()
    {
        if (!isDone && GPS.latitude > BC.savedLatitude || GPS.latitude < BC.savedLatitude)
        {
            FLM.FL_Start();
            isDone = true;
        }
        else if (!isDone && GPS.longitude > BC.savedLongitude || GPS.longitude < BC.savedLongitude)
        {
            FLM.FL_Start();
            isDone = true;
        }
        else
        {
            FLM.FL_Stop();
        }
    }
        
}
