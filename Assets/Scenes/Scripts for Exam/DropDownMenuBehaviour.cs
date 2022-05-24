using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DropDownMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _dropdown;
    private InputBehaviour _IB;
    // Start is called before the first frame update
    void Start()
    {
        _IB = GetComponent<InputBehaviour>();
        _dropdown.onValueChanged.AddListener(DropDownChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropDownChanged(int index)
    {
        if (index == 0)
        {
            _IB.groupHome.SetActive(true);
            _IB.groupWork.SetActive(false);

            //Do something change active layout group
        }
        else if(index == 1)
        {
            _IB.groupHome.SetActive(false);
            _IB.groupWork.SetActive(true);

        }
    }
}
