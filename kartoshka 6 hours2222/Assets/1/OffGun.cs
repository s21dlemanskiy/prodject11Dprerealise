using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffGun : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (!(this.GetComponent<FollowPlayer>().player))
        {

            this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);

        }
        else {
            this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);

        }
        
    }
}
