using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool soloGame = true;
    bool paused = false;
    public GameObject cursor;
    public GameObject Pause_Main;
    public GameObject[] otherMenus;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }
    public void Pause(){
         Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        if(soloGame){
            Time.timeScale = 0;
        }
        Pause_Main.SetActive(true);
        cursor.SetActive(false);
        paused = !paused;
    }
    public void Resume(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if(soloGame){
            Time.timeScale = 1;
        }
        foreach(GameObject menu in otherMenus){
            menu.SetActive(false);
        }
        cursor.SetActive(true);
        Pause_Main.SetActive(false);
        paused = !paused;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log(Time.timeScale);
            if(paused){
                Resume();
            }else{
                Pause();
            }
        }
    }
}
