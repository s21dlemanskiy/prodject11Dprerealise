using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreScript : MonoBehaviour
{
    public int count;
    public int[] scor = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public bool localstop = false;
    public bool globalstop;
    public float times;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (localstop != globalstop)
        {

            //Debug.LogWarning(globalstop.ToString() + localstop.ToString());
            if (globalstop)
            {
                localstop = true;          // тут то что происъодит при остановки времени
                GameObject.Find("EndManager").transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                localstop = false;  
                GameObject.Find("EndManager").transform.GetChild(0).gameObject.SetActive(false) ;       //тут то что происъодит при запуске времени
                StartNewCercle();
            }
        }
    }

    public int findPlace(int num){
            int s = 0;
            foreach(int sc in scor){
                if(sc > scor[num]){
                    s += 1;
                }
            }
            return s + 1;
        }
    public void StartNewCercle() {
        for (int i = 0; i < 10; i++)
        {
            if (GameObject.Find("Player" + i.ToString()))
            {
                GameObject playerg = GameObject.Find("Player" + i.ToString());
                if (playerg.GetComponent<PlayerMovement>().issrv())
                {
                    Debug.LogWarning("66666");
                    Vector3[] arr = GameObject.Find("Camera1").GetComponent<FollowPlayer>().spowns;
                    Vector3 pos;
                    if (arr.Length == 0)
                    {
                        pos = new Vector3(0, 0, 0);
                        Debug.LogError("No spawn point you must add it in Camera1/FollowPlayer.cs");
                    }
                    else
                    {
                        pos = arr[UnityEngine.Random.Range(0, arr.Length)];
                    }
                    playerg.transform.position = pos;
                    this.transform.rotation = Quaternion.identity;
                    playerg.GetComponent<PlayerMovement>().RcpKillPos(this.name, pos);

                    GameObject.Find("Camera1").GetComponent<FollowPlayer>().OffRed();
                    playerg.gameObject.layer = 13;
                    playerg.GetComponent<PlayerMovement>().layer2(13);
                    playerg.GetComponent<PlayerMovement>().strtImmortalityTimer();
                }
                playerg.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }

    }
}
