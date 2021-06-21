using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBulletShoot : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity += transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
