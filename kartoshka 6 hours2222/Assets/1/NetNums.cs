using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetNums : NetworkBehaviour
{
    public static float timertime = 300f;
    public static float timertime2 = 5f;
    public int MaxHealth = 100;


    [ClientRpc]
    public void RpcAskToRanamePlayer()               //переименововаем везде player и изменяем num в Cam
    {
        GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
        if (GameObject.Find("Aplayer(Clone)"))
        {
            // берем всех playerов и с последнего нумеруем для добавления 1 плаера это просто переименует последнего а вот для свежеподключенного клиента должно назвать всех плееров на сцене
            for (int i = a.Length - 1; i > -1; i -= 1)
            {
                if (a[i].name == "Aplayer(Clone)")
                {
                    a[i].name = "Player" + (i).ToString();
                    if (GameObject.Find("Camera1").GetComponent<FollowPlayer>().num == -1)
                    {
                        GameObject.Find("Camera1").GetComponent<FollowPlayer>().num = i;
                        GameObject.Find("Camera1").GetComponent<FollowPlayer>().SetAsAPlayer(a[i]);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("smb not found noname");
            if (isServer)
            {
                Debug.LogWarning("smb not found noname but it is server!!!!!!!!!!!!");

            }
        }

    }

    SyncList<int> _score = new SyncList<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //метод не выполнится, если старое значение равно новому

    [Server] //обозначаем, что этот метод будет вызываться и выполняться только на сервере
    public void Addscr(int num)
    {
        _score[num]++;
    }

    void SyncHealth(Mirror.SyncList<int> oldValue, Mirror.SyncList<int> newValue) //обязательно делаем два значения - старое и новое. 
    {
        _score = newValue;
    }




    [Command]
    void Addscore2(int value)
    {
        AddScore(value);
    }









    public void AddScore(int num)
    {
        if (hasAuthority)
        {
            if (isServer)
            {
                Addscr(num);
            }
            else
            {
                Addscore2(num);
            }
        }

    }



    [Server]
    public void UPscore()
    {
        int[] a = new int[_score.Count];
        _score.CopyTo(a, 0);
        Rpcchgescore(a);
    }

    [Command]
    public void AskToUpScore()
    {
        Upscr();

    }



    public void Upscr()
    {
        if (isServer)
        {
            UPscore();
        }
        else
        {
            AskToUpScore();
        }

    }

    //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    //vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
    [ClientRpc]
    public void Rpcchgescore(int[] d) //обязательно ставим Rpc в начале названия метода
    {
        GameObject.Find("Playernums2").GetComponent<ScoreScript>().scor = d;
    }

    //...........................................................................................................
    //........................................................................................................
    [SyncVar(hook = "OnStop")]
    public bool _globalstop = false;


    public void GloblStopBool(bool q)
    {
        if (isServer)
        {
            _globalstop = q;
            RpcChngStop(q);
            if (q)
            {
                _timer = timertime2;
                RcpChngTime(_timer);

            }
            else
            {
                _timer = timertime;
                RcpChngTime(_timer);

            }

        }


    }

    void OnStop(bool a, bool b)
    {
        _globalstop = b;
    }

    [SyncVar(hook = "ChngTimer")]
    public float _timer;


    public void ChngTimer(float a, float b)
    {

        _timer = b;
    }

    public void FixedUpdate()
    {
        if (isServer)
        {
            _timer -= Time.deltaTime;
            //Debug.LogWarning(_timer.ToString()+"......" + timertime.ToString());
            if (_timer < 0) {
                _timer = timertime;
                if (_globalstop) {
                    CleanScore();
                    CleanhealthandOffGun();
                    for (int i = 0; i < 10; i++)
                    {
                        if (GameObject.Find("Player" + i.ToString()))
                        {
                            GameObject.Find("Player" + i.ToString()).GetComponent<HealthController>().health = MaxHealth;
                        }
                    }

                }
                GloblStopBool(!_globalstop);
            }
            RcpChngTime(_timer);
        }
    }

    [ClientRpc]
    public void RcpChngTime(float timer)
    {
        GameObject.Find("Playernums2").GetComponent<ScoreScript>().times = timer;
    }
    [ClientRpc]
    public void RpcChngStop(bool a) {
        GameObject.Find("Playernums2").GetComponent<ScoreScript>().globalstop = a;

    }


    public void CleanScore() {

        if (isServer) {
            for (int i = 0; i< 10; i++) {
                _score[i] = 0;
            }
            int[] a = new int[_score.Count];
            _score.CopyTo(a, 0);
            Rpcchgescore(a);
        }
    }


    [ClientRpc]
    public void CleanhealthandOffGun()
    {
        GameObject.Find("Camera1").transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.SetActive(true);  //pistolred
        GameObject.Find("Camera1").transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.SetActive(false); //mushingun
        GameObject.Find("Camera1").transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.SetActive(false); //shotgun
    }
}
