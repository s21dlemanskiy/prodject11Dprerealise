using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSensitivitySlider : MonoBehaviour
{
    public Slider slider;
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
        PlayerPrefs.SetFloat("sensitivity", slider.value);
    }
}
