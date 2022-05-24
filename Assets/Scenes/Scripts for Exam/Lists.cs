using System.Collections.Generic;
using UnityEngine;

public class Lists : SingletonPattern<Lists>
{   
    //The list of GameObjects that are active in the "Home" dropdown
    public List<GameObject> roomsHome = new List<GameObject>();

    // The list of GameObjects that are active in the "Work" dropdown
    public List<GameObject> roomsWork = new List<GameObject>();

    private void Update()
    {
        Debug.Log(roomsHome.Count);
        Debug.Log(roomsWork.Count);
    }
}
