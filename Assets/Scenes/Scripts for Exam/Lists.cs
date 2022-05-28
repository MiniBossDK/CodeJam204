using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Some of the commented out text contains code from: https://www.youtube.com/watch?v=mRis4dRkZtE
public class Lists : SingletonPattern<Lists>
{
    //The list of GameObjects that are active in the "Home" dropdown
    public List<GameObject> roomsHome = new List<GameObject>();

    // The list of GameObjects that are active in the "Work" dropdown
    public List<GameObject> roomsWork = new List<GameObject>();

    // TEST
    //public List<string> roomNamesHome = new List<string>();

    //public List<string> roomNamesWork = new List<string>();

    //private int _savedListCount;

    // private InputBehaviour _IB;

    //private GameObject _roomText;
    private void Start()
    {
        //_IB = GetComponent<InputBehaviour>();
        // TEST
        //_roomText = _IB.room;
    }
    private void Update()
    {
        Debug.Log(roomsHome.Count);
        Debug.Log(roomsWork.Count);
    }


   /* public void LoadRooms()
    {
        //roomNamesHome.Clear();
        //roomNamesWork.Clear();
        _savedListCount = PlayerPrefs.GetInt("Count");
        for (int i = 0; i < _savedListCount; i++)
        {
             _IB.roomText.text = PlayerPrefs.GetString("roomNameWork" + i);
            roomNamesWork.Add(_IB.roomText.ToString());
            //string work = PlayerPrefs.GetString("")
            foreach (var room in roomNamesHome)
            {

                _IB.roomText = _roomText.GetComponentInChildren<TextMeshProUGUI>();
                _IB.roomText.text = room;
                Debug.Log("Hello");
            }
        }
    }*/
}
