using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InputBehaviour : MonoBehaviour
{
    //Button that adds room to scene as well as adding it to the list
    [SerializeField]
    private Button createRoom;

    //The Button that clears the room list and deletes instantiated Prefabs
    [SerializeField]
    private Button clearRoom;

    [SerializeField]
    private TMP_InputField inputField;

    [SerializeField]
    private GameObject _room;

    private Lists lists;

    public GameObject groupHome;

    public GameObject groupWork;

    private TextMeshProUGUI roomText;

    //------------------------------------------------------------------------------------------------------------------

    //EXPERIMENTAL
    //public List<TextMeshProUGUI> rooms = new List<TextMeshProUGUI>();

    //------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        lists = Lists.Instance;
        createRoom.onClick.AddListener(AddRoom);
        clearRoom.onClick.AddListener(ClearList);
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
            GameObject prefab = Instantiate(_room, groupHome.transform);
            lists.roomsHome.Add(prefab);
            roomText = prefab.GetComponentInChildren<TextMeshProUGUI>();

            roomText.text = inputField.text;
        }

        else if (groupWork.activeInHierarchy == true)
        {
            GameObject prefab = Instantiate(_room, groupWork.transform);
            lists.roomsWork.Add(prefab);
            roomText = prefab.GetComponentInChildren<TextMeshProUGUI>();

            roomText.text = inputField.text;
            for(int i = 0; i < lists.roomsWork.Count; i++)
            {
                PlayerPrefs.SetString("roomName" + i, roomText.ToString());
            }
        }
    }
}
