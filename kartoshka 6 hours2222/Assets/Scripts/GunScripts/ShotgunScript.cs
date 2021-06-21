using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShotgunScript : MonoBehaviour
{
    public int id;
    public GameObject prefab;
    public GameObject player;
    public GameObject barrel1;
    public GameObject barrel2;
    public Camera cam;
    public float delay;
    public float fireRate;
    public float shards;
    public float angle;
    public int depth = 1;
    public Animator anim;
    AudioSource audioData;
    WeaponSystem wSystem;
    Coroutine shooter;
    LayerMask aimLayerMask;
    float angleR;
    // Start is called before the first frame update
    void Start()
    {
        angleR = angle / 57.2958f;
        aimLayerMask = ~(1 << LayerMask.NameToLayer ("Pickup")); 
        //StartCoroutine(Shoot());
        audioData = GetComponent<AudioSource>();
    }
    void OnEnable()
    {
        Debug.Log("PrintOnEnable: script was enabled");
        anim.Play("Switching");
        StopAllCoroutines();
        shooter = StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(delay);
        while(true){
            if(Input.GetKey(KeyCode.Mouse0) && !(GameObject.Find("Playernums2").GetComponent<ScoreScript>().localstop))
            {
                if (!wSystem)
                {
                    wSystem = player.GetComponent<WeaponSystem>();
                }
                if (wSystem.ammoCheck(id)){
                    Aim();
                    if (GameObject.Find("Camera1").GetComponent<FollowPlayer>().player)
                    {

                        for (int i = 0; i < shards; i++)
                        {
                            Vector3 directionV = UnityEngine.Random.insideUnitCircle * (float)Math.Tan(angleR);
                            directionV.z = 1; // circle is at 1 unit 
                            Quaternion direction = Quaternion.LookRotation(directionV);
                            direction = barrel1.transform.rotation * direction;
                            GameObject.Find("Camera1").GetComponent<FollowPlayer>().player.GetComponent<PlayerMovement>().spwnbul3(barrel1.transform.position, direction, Int32.Parse(GameObject.Find("Camera1").GetComponent<FollowPlayer>().player.name[6].ToString()));
                        }
                        for (int i = 0; i < shards; i++)
                        {
                            Vector3 directionV = UnityEngine.Random.insideUnitCircle * (float)Math.Tan(angleR);
                            directionV.z = 1; // circle is at 1 unit 
                            Quaternion direction = Quaternion.LookRotation(directionV);
                            direction = barrel2.transform.rotation * direction;
                            GameObject.Find("Camera1").GetComponent<FollowPlayer>().player.GetComponent<PlayerMovement>().spwnbul3(barrel2.transform.position, direction, Int32.Parse(GameObject.Find("Camera1").GetComponent<FollowPlayer>().player.name[6].ToString()));
                        }
                    }

                    if(audioData){
                        audioData.Play(0);
                    }
                    anim.Play("Recoil");
                    yield return new WaitForSeconds(fireRate);
                }
            }
            yield return null;
        }
    }
    float SmartRayCast(Ray ray, int depth, out RaycastHit hit){
        //!!!!!!!!!!!!!!!!!!!!!!!!add distance!!!!!!!!!!!!!!!!!!!!
        if(Physics.Raycast(ray, out hit, 1000f, aimLayerMask)){
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
            barrel1.transform.LookAt(ray.origin + ray.direction*dist);
            barrel2.transform.LookAt(ray.origin + ray.direction*dist);
        }
        else{
            barrel1.transform.localRotation = Quaternion.identity;
            barrel2.transform.localRotation = Quaternion.identity;
        }
    }

}


