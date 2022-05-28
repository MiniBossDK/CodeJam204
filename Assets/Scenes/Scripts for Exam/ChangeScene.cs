using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public Button bttn;
    public void Start()
    {
        bttn.onClick.AddListener(SceneChange);
    }
    public void SceneChange()
    {
        SceneManager.LoadScene(1);
    }
}
