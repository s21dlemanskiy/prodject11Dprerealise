using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 5;
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        var hc = collisionInfo.gameObject.GetComponent<HealthController>();
        if(hc){
            if (collisionInfo.gameObject.GetComponent<PlayerMovement>())
            {
                if (collisionInfo.gameObject.GetComponent<PlayerMovement>().issrv())
                {
                    hc.TakeDamage(damage, num);
                }
            }
        }
        Debug.Log(collisionInfo.gameObject.name);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
