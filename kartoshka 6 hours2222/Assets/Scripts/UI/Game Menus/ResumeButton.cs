using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour
{
    public PauseMenu pm;
    public Button button;
    // Start is called before the first frame update
    void Start(){
        button.onClick.AddListener(Resuming);
    }


    void Resuming()
    {
        pm.Resume();
    }

    // Update is called once per frame
    
}
