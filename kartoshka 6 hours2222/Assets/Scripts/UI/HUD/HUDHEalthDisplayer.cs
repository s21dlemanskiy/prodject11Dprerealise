using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHEalthDisplayer : MonoBehaviour
{
    public Text text;
    public HealthController healthScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthScript){
            text.text = (healthScript.health).ToString();
        }
        else{
            if (GameObject.Find("Camera1").GetComponent<FollowPlayer>().player){
                healthScript = GameObject.Find("Camera1").GetComponent<FollowPlayer>().player.GetComponent<HealthController>();
            }
        }
    }
}
