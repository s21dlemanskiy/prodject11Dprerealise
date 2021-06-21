using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health = 10;
    public bool NotToDie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(int amount, int num){
        this.gameObject.GetComponent<PlayerMovement>().TakeDamage3( - amount, num);
        
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}


