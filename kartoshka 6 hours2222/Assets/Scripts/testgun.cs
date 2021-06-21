using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testgun : MonoBehaviour
{
    public GameObject prefab;
    public GameObject player;
    public Camera cam;
    public float delay;
    public float fireRate;
    public int depth = 1;
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
        audioData = GetComponent<AudioSource>();
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delay);
        while(true){
            if(Input.GetKey(KeyCode.Mouse0)){
                Aim();
                Instantiate(prefab, transform.position, transform.rotation);
                if(audioData){
                    audioData.Play(0);
                }
                yield return new WaitForSeconds(fireRate);
            }
            yield return null;
        }
    }
    float SmartRayCast(Ray ray, int depth, out RaycastHit hit){
        //!!!!!!!!!!!!!!!!!!!!!!!!add distance!!!!!!!!!!!!!!!!!!!!
        if(Physics.Raycast(ray, out hit)){
            Debug.Log(hit.transform.gameObject.name);
            if(hit.transform.gameObject.tag != "Portal"){
            return hit.distance;
            }else{
                if(depth > 0){
                    RaycastHit newHit;
                    GameObject portalEntrance = hit.transform.gameObject;
                    PortalTeleporter portalExitScript = portalEntrance.GetComponent<PortalTeleporter>();
                    GameObject portalExit = portalExitScript.receiver.gameObject;
                    Vector3 portalToHit = hit.point - portalEntrance.transform.position;
                    float rotationDiff = portalExit.transform.eulerAngles.y - portalEntrance.transform.eulerAngles.y + 180;
                    Vector3 newRayOrigin = Quaternion.Euler(0, rotationDiff, 0) * portalToHit + portalExit.transform.position;
                    Vector3 newRayDirection = Quaternion.Euler(0, rotationDiff, 0) * ray.direction;
                    Ray newRay = new Ray(newRayOrigin, newRayDirection);
                    float newDist = SmartRayCast(newRay, depth-1, out newHit);
                    if(newDist == -1){
                        return -1;
                    }else{
                        return hit.distance + newDist;
                    }
                }else{
                    return hit.distance;
                }
            }
        }else{
            return -1f;
        }
    }
    void Aim()
    {
        float screenX = Screen.width / 2;
        float screenY = Screen.height / 2;
 
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(screenX, screenY));
        float dist = SmartRayCast(ray, depth, out hit);
        if (dist != -1){
            transform.LookAt(ray.origin + ray.direction*dist);
        }
        else{
            transform.localRotation = Quaternion.identity;
        }
    }

}
