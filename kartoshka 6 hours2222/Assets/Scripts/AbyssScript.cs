using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        var hc = collisionInfo.gameObject.GetComponent<HealthController>();
        if(hc){
            if (collisionInfo.gameObject.GetComponent<PlayerMovement>())
            {
                if (collisionInfo.gameObject.GetComponent<PlayerMovement>().issrv())
                {
                    hc.TakeDamage(10000000, -1);
                }
            }
        }
        Debug.Log(collisionInfo.gameObject.name);
    }
}
