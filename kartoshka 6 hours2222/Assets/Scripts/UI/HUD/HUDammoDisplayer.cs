using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDammoDisplayer : MonoBehaviour
{
    public Text text;
    public WeaponSystem weaponSystem;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponSystem){
            text.text = weaponSystem.ammo[id].ToString();
        }
        else{
            if (GameObject.Find("Camera1").GetComponent<FollowPlayer>().player){
                weaponSystem = GameObject.Find("Camera1").GetComponent<FollowPlayer>().player.GetComponent<WeaponSystem>();
            }
        }
    }
}
