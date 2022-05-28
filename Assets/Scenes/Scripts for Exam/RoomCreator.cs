using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RoomCreator : MonoBehaviour
{
    public TextMeshProUGUI roomName;
    public int roomID;

    public RoomCreator(TextMeshProUGUI text, int id)
    {
        text = roomName;
        id = roomID;
    }
}
