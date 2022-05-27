using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSManager : SingletonPattern<GPSManager>
{
    public float latitude;
    public float longitude;
    private int maxWait = 20;
    private float updateWaitTime = 2f;
    IEnumerator coroutine;

    // the foundation of this code has been taken from the Unity Manual on GPS location, but since modified to fit the purpose of my
    // application. Link to Unity Manual where code is from: https://docs.unity3d.com/ScriptReference/LocationService.Start.html
    private IEnumerator Start()
    {
        coroutine = UpdateLocation();

        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            yield break;

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }

        StartCoroutine(coroutine);
    }

    /// <summary>
    /// This method will update the location coordinates continously 
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateLocation()
    {
        WaitForSeconds updateTime = new WaitForSeconds(updateWaitTime);

        while (true)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            yield return updateTime;
        }

        void StopGPS()
        {
            Input.location.Stop();
            StopCoroutine(coroutine);
        }

        /// stops the GPS when the application is closed
        void OnDisable()
        {
            StopGPS();
        }
    }
}
