using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int id;
    public bool addWeapon;
    public bool canitake = true;
    public int ammo;
    public bool hasBarrel = true;
    private void Start()
    {
        canitake = true;
    }
    public IEnumerator SetTrue()
    {
        yield return new WaitForSeconds(15f);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        
        this.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
        if (hasBarrel){
        this.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        canitake = true;
    }
        void OnTriggerEnter(Collider other)
    {
         var ws = other.gameObject.GetComponent<WeaponSystem>();
        if (ws)
        {
            if (canitake && (other.name == "Player" + GameObject.Find("Camera1").GetComponent<FollowPlayer>().num.ToString()))
            {
                if (addWeapon)
                {
                    ws.addWeapon(id);
                }
                ws.addAmmo(id, ammo);
                ws.setWeapon(id);
                StartCoroutine(SetTrue());
                canitake = false;
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                if (hasBarrel){
                this.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                Debug.LogWarning(other.name);
            }
        }
    }
}
