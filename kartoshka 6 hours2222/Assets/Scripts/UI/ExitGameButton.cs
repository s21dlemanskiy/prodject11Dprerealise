using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start(){
        button.onClick.AddListener(Quit);
    }


    void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    
}
