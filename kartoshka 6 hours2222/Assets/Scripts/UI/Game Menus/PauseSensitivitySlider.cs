using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSensitivitySlider : MonoBehaviour
{
    public Slider slider;
    public PlayerMovement playerMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("sensitivity")){
            slider.value = PlayerPrefs.GetFloat("sensitivity");
        }
        slider.onValueChanged.AddListener(delegate {SetSensitivity();});
    }

    // Update is called once per frame
    void SetSensitivity()
    {
        playerMovementScript.SetSensitivity(slider.value);
        PlayerPrefs.SetFloat("sensitivity", slider.value);
    }
}
