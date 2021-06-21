using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject[] weapons;
    public int[] ammo;
    public bool[] exists;
    public int currentId = 0;
    float scroll;
    public int weaponCount;

    public void Start(){
        weaponCount = ammo.Length;
    }

    public bool setWeapon(int id){
        if (exists[id] && ammo[id] != 0){
            weapons[currentId].SetActive(false);
            weapons[id].SetActive(true);
            currentId = id;
            return true;
        }
        return false;
    }

    public bool addAmmo(int id, int quantity){
        if(exists[id]){
            ammo[id] += quantity;
            return true;
        }
        return false;
    }
    public void addWeapon(int id){
        exists[id] = true;

    }
    public bool ammoCheck(int id){
        if (exists[id]){
            if (ammo[id] == -1){
                return true;
            }
            if (ammo[id] != 0){
                ammo[id] -= 1;
                return true;
            }
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        var newId = currentId;
        if (ammo[newId] == 0){
            while(ammo[newId] == 0 || !exists[newId]){
                newId -= 1;
            }
        }
        scroll = Input.GetAxis("Mouse ScrollWheel");
        
        if (scroll < 0){
            newId -= 1;
            if (newId < 0){
                newId = weaponCount - 1;
            }
            while(ammo[newId] == 0 || !exists[newId]){
                newId -= 1;
                if (newId < 0){
                newId = weaponCount - 1;
                }
            }
        }
        if (scroll > 0){
            newId += 1;
            if (newId >= weaponCount){
                newId = 0;
            }
            while(ammo[newId] == 0 || !exists[newId]){
                newId += 1;
                if (newId >= weaponCount){
                    newId = 0;
                }
            }
        }

        if (currentId != newId){
            setWeapon(newId);
        }
    }
}
