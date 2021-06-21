using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMen : NetworkManager
{
    public bool playerSpawned;
    NetworkConnection connection;
    public GameObject Playernums;
    bool playerConnected;
    List<float> posit;


    

    public void OnCreateCharacter(NetworkConnection conn, PosMessage message)
    { 
        GameObject go = Instantiate(playerPrefab, message.vector3, Quaternion.identity); //�������� �� ������� ������� gameObjec
        //go.name = "Player" + GameObject.Find("Playernums2").GetComponent<ScoreScript>().count;
        NetworkServer.AddPlayerForConnection(conn, go); //������������ gameObject � ���� ������� �������� � ���������� ���������� �� ���� ��������� �������
        GameObject.Find("Playernums(Clone)").GetComponent<NetNums>().RpcAskToRanamePlayer();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        GameObject j = Instantiate(Playernums, new Vector3(0, 0, 0), Quaternion.identity);
        NetworkServer.Spawn(j);
        NetworkServer.RegisterHandler<PosMessage>(OnCreateCharacter); //���������, ����� struct ������ ������ �� ������, ����� ���������� �����
    }


    public void ActivatePlayerSpawn()
    {

        
        Vector3[] arr = GameObject.Find("Camera1").GetComponent<FollowPlayer>().spowns;
        Vector3 pos;
        if (arr.Length == 0)
        {
            pos = new Vector3(0, 0, 0);
            Debug.LogError("No spawn point you must add it in Camera1/FollowPlayer.cs");
        }
        else
        {
            pos = arr[Random.Range(0, arr.Length)];
        }
        PosMessage m = new PosMessage() { vector3 = pos}; //������� struct ������������� ����, ����� ������ ����� � ���� ��� ������ ���������
        connection.Send(m); //�������� ��������� �� ������ � ������������ ������
        playerSpawned = true;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        connection = conn;
        playerConnected = true;
        //if()
    }

    private void Update()
    {
        if (!playerSpawned && playerConnected) //&& Input.GetKeyDown(KeyCode.Space)
        {
            ActivatePlayerSpawn();
        }
    }










}

public struct PosMessage : NetworkMessage //����������� �� ���������� NetworkMessage, ����� ������� ������ ����� ������ �����������
{
    public Vector3 vector3; //������ ������������ Property


}




