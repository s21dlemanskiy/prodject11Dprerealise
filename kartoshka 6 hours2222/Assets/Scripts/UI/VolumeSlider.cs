using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public float defaultVolume = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("volume")){
            slider.value = PlayerPrefs.GetFloat("volume");
            AudioListener.volume = slider.value;
        }else{
            AudioListener.volume = defaultVolume;
        }
        slider.onValueChanged.AddListener(delegate {SetVolume();});
    }

    // Update is called once per frame
    void SetVolume()
    {
        AudioListener.volume = slider.value;
        PlayerPrefs.SetFloat("volume", slider.value);
    }
}
