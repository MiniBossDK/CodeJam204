using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Some of the commented out text contains code from: https://www.youtube.com/watch?v=mRis4dRkZtE
public class InputBehaviour : MonoBehaviour
{
    //Button that adds room to scene as well as adding it to the list
    [SerializeField]
    private Button createRoom;

    //The Button that clears the room list and deletes instantiated Prefabs
    [SerializeField]
    private Button clearRoom;

    //The inputfield used to change location
    [SerializeField]
    private TMP_InputField inputField;

    //Prefab 
    public GameObject room;

    //Script for the lists of objects
    private Lists lists;

    //Layout group for Home
    public GameObject groupHome;

    //Layout Group for Work 
    public GameObject groupWork;

    //used to get the component from the child object of the room
    private TextMeshProUGUI roomText;

    void Start()
    {
        lists = Lists.Instance;
        createRoom.onClick.AddListener(AddRoom);
        clearRoom.onClick.AddListener(ClearList);
        // TEST
        //lists.LoadRooms();
    }


    /// <summary>
    /// Clears the currently added rooms and deletes every prefab from the scene
    /// </summary>
    private void ClearList()
    {
        if (groupHome.activeInHierarchy)
        {
            foreach (GameObject room in lists.roomsHome)
            {
                Destroy(room);
            }
            lists.roomsHome.Clear();
        }

        else if (groupWork.activeInHierarchy)
        {
            foreach (GameObject room in lists.roomsWork)
            {
                Destroy(room);
            }
            lists.roomsWork.Clear();
        }

    }


    /// <summary>
    /// Adds a room depending on the active location
    /// </summary>
    private void AddRoom()
    {
        if (groupHome.activeInHierarchy == true)
        {

            //This is for the newly instantiated object to be built
            GameObject prefab = Instantiate(room, groupHome.transform);
            roomText = prefab.GetComponentInChildren<TextMeshProUGUI>();
            lists.roomsHome.Add(prefab);
            roomText.text = inputField.text;
        }

        else if (groupWork.activeInHierarchy == true)
        {
            //This is for the newly instantiated object to be built
            GameObject prefab = Instantiate(room, groupWork.transform);
            lists.roomsWork.Add(prefab);
            roomText = prefab.GetComponentInChildren<TextMeshProUGUI>();
            roomText.text = inputField.text;
        }
        /*{     // Test to try PlayerPrefs
            for(int i = 0; i > lists.roomNamesWork.Count; i++)
            {
                PlayerPrefs.SetString("roomNameWork" + i, lists.roomNamesWork[i]);
            }
            PlayerPrefs.SetInt("Count", lists.roomNamesWork.Count);

          /*if(!lists.roomNamesHome.Contains(roomText.text))
            {
                lists.roomNamesHome.Add(roomText.text);
                GameObject prefab = Instantiate(room, groupHome.transform);
                lists.roomsHome.Add(prefab);
            }*/
    }
}
