using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSwitchButton : MonoBehaviour
{
    public GameObject menu;
    public GameObject targetMenu;
    public Button button;
    // Start is called before the first frame update
    void Start(){
        button.onClick.AddListener(Switching);
    }


    void Switching()
    {
        targetMenu.SetActive(true);
        menu.SetActive(false);
    }
    
}
