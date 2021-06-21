using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetPist : NetworkBehaviour
{
    public GameObject prefab; 

    [Server]
    public void spwnbul1(Vector3 Pos, Quaternion Rot, int num)
    {
        GameObject bul = Instantiate(prefab, Pos, Rot);
        NetworkServer.Spawn(bul);
        bul.GetComponent<BulletScript>().num = num;

    }
    [Command]
    public void spwnbul2(Vector3 Pos, Quaternion Rot, int num)
    {
        spwnbul3(Pos, Rot, num);
    }



    public void spwnbul3(Vector3 Pos, Quaternion Rot, int num)
    {
        if (isServer)
        {
            spwnbul1(Pos, Rot, num);
        }
        else
        {
            spwnbul2(Pos, Rot, num);
        }
    }
}
