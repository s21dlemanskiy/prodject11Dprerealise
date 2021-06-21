using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRenderer : MonoBehaviour
{
    public Camera portalCamera;
    public int maxRecursions = 2;
    
    public int debugTotalRenderCount;
    public int maxDist = 100;
    private Camera mainCamera;
    private PortalVisualiser[] allPortals;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        allPortals = FindObjectsOfType<PortalVisualiser>();
    }
    


    private void LateUpdate()
    {
        //Debug.Log("PRERENDER");
        debugTotalRenderCount = 0;

        foreach (var portal in allPortals)
        {
            if (Vector3.Distance(player.transform.position, portal.transform.position) <= maxDist){
            portal.DeepRender(
            mainCamera.transform.position,
            mainCamera.transform.rotation,
            out _,
            out _,
            out var renderCount,
            portalCamera,
            0,
            maxRecursions);

        debugTotalRenderCount += renderCount;
        }
        }
    }
    private void Update()
    {
        RenderTexturePool.Instance.ReleaseAllTextures();
    }
    // Update is called once per frame
}
