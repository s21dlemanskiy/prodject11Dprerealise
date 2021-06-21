using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraMovement : MonoBehaviour
{
	public Transform Camera;
	public Transform PlayerCamera;
	public Transform Portal;
	public Transform OtherPortal;

    // Update is called once per frame
    void LateUpdate()
    {
		Camera.localPosition = new Vector3 (OtherPortal.position.x - PlayerCamera.position.x,  PlayerCamera.position.y - OtherPortal.position.y , OtherPortal.position.z - PlayerCamera.position.z );
		float rotationalDiff =  OtherPortal.eulerAngles.y;
		Camera.localPosition = Quaternion.AngleAxis(-OtherPortal.eulerAngles.y, Vector3.up) * Camera.localPosition;
		//Camera.localPosition = Quaternion.AngleAxis(-rotationalDiff, Vector3.up) * Camera.localPosition;
		//float angularDifferenceBetweenPortalRotations = Quaternion.Angle(Portal.rotation, OtherPortal.rotation);

		//Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		//Vector3 newCameraDirection = portalRotationalDifference * -PlayerCamera.forward;
		//Camera.transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
		
		Camera.transform.localRotation = Quaternion.Euler (PlayerCamera.transform.rotation.eulerAngles.x, 180f + PlayerCamera.transform.rotation.eulerAngles.y  - OtherPortal.eulerAngles.y, PlayerCamera.transform.rotation.eulerAngles.z );
		//Debug.Log((PlayerCamera.transform.rotation.eulerAngles.x, 180f + PlayerCamera.transform.rotation.eulerAngles.y  - OtherPortal.eulerAngles.y, PlayerCamera.transform.rotation.eulerAngles.z ));
    }
}
