 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject deathScreen;
    public GameObject player;
    public int num = -1;
    public Vector3[] spowns;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            this.transform.position = player.transform.position + new Vector3(0, 0, 0);
            this.transform.rotation = player.transform.rotation;
        }
    }


    public IEnumerator ImmortalityTimer()
    {
        yield return new WaitForSeconds(5f);
        if (this.player)
        {
            this.player.GetComponent<HealthController>().NotToDie = false;
            this.player.layer = 12;
        }
    }



    public void SetAsAPlayer(GameObject playerk)
    {
        player = playerk;
        player.GetComponent<PlayerMovement>().cam = this.transform.GetChild(0).gameObject;
        this.transform.Rotate(new Vector3(-90f, 0, 0));
        this.transform.position = player.transform.position;

        k();


        this.player.GetComponent<PlayerMovement>().StartCaruntin(this.player.name);
    }

    public void k()
    { //����������� ��������� ��������
        GameObject.Find("Camera1").transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.SetActive(true); //convas(menu)
        GameObject.Find("Camera1").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);  //pistolred
        GameObject.Find("Camera1").transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false); //mushingun
        GameObject.Find("Camera1").transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(false); //shotgun
        GameObject.Find("Camera1").transform.GetChild(0).localPosition = new Vector3(0, 0.75f, 0); //������������ cam in cam1
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<CursorScript>().enabled = true;
        //���������� �����:
        player.GetComponent<WeaponSystem>().weapons[0] = this.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject; //pistol
        player.GetComponent<WeaponSystem>().weapons[1] = this.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject; //mushingun
        player.GetComponent<WeaponSystem>().weapons[2] = this.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject; //shotgun


        this.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<PistolScript>().player = player;
        this.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).GetComponent<PistolScript>().player = player;
        this.transform.GetChild(0).gameObject.transform.GetChild(4).GetComponent<ShotgunScript>().player = player;
    }


    public void OffRed()
    {
        deathScreen.SetActive(false);
    }

    public void OnRed()
    {
        deathScreen.SetActive(true);
    }
}


   



