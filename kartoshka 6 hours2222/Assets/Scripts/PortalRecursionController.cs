using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRecursionController : MonoBehaviour
{
    public PortalRecursionController[] visiblePortals;
    public Transform OtherPortal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        // Linked portals
    
        if (OtherPortal != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, OtherPortal.transform.position);
        }
    
        // Visible portals
        Gizmos.color = Color.blue;
        foreach (var visiblePortal in visiblePortals)
        {
            Gizmos.DrawLine(transform.position, visiblePortal.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
