using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

	public Transform player;
	public Transform receiver;

	private bool playerIsOverlapping = false;

	// Update is called once per frame
	void Update () {
		//Debug.Log(playerIsOverlapping);
		/* if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{
				// Teleport him!
				float rotationDiff = receiver.eulerAngles.y - transform.eulerAngles.y;
				rotationDiff += 180;
				player.Rotate(Vector3.up, rotationDiff);
				PlayerMovement pm = player.GetComponent<PlayerMovement>();
				pm.AddScriptRotation(rotationDiff);
				Debug.Log((rotationDiff,true ,receiver.eulerAngles.y, transform.eulerAngles.y));
				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.position = receiver.position + positionOffset;

				playerIsOverlapping = false;
			}
		} */
	}
	void OnTriggerStay(Collider other){
		if (other.tag == "Bullet")
		{
			Vector3 bulletToPlayer = other.gameObject.transform.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, bulletToPlayer);
			if (dotProduct < 0f)
			{
				// Teleport the bullet!
				float rotationDiff = receiver.eulerAngles.y - transform.eulerAngles.y;
				rotationDiff += 180;
				other.transform.Rotate(Vector3.up, rotationDiff);
				Debug.Log((rotationDiff,true ,receiver.eulerAngles.y, transform.eulerAngles.y));
				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * bulletToPlayer;
				other.transform.position = receiver.position + positionOffset;
				other.gameObject.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, rotationDiff, 0) * other.gameObject.GetComponent<Rigidbody>().velocity;
			}
		}
		if (other.tag == "Bullet")
		{
			Vector3 bulletToPlayer = other.gameObject.transform.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, bulletToPlayer);
			if (dotProduct < 0f)
			{
				// Teleport the bullet!
				float rotationDiff = receiver.eulerAngles.y - transform.eulerAngles.y;
				rotationDiff += 180;
				other.transform.Rotate(Vector3.up, rotationDiff);
				Debug.Log((rotationDiff,true ,receiver.eulerAngles.y, transform.eulerAngles.y));
				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * bulletToPlayer;
				other.transform.position = receiver.position + positionOffset;
				
				other.gameObject.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, rotationDiff, 0) * other.gameObject.GetComponent<Rigidbody>().velocity;
			}
		}
		if (other.tag == "Player")
		{
			Vector3 bulletToPlayer = other.gameObject.transform.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, bulletToPlayer);
			if (dotProduct < 0f)
			{
				// Teleport the player!
				float rotationDiff = receiver.eulerAngles.y - transform.eulerAngles.y;
				rotationDiff += 180;
				other.transform.Rotate(Vector3.up, rotationDiff);
				Debug.Log((rotationDiff,true ,receiver.eulerAngles.y, transform.eulerAngles.y));
				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * bulletToPlayer;
				PlayerMovement pm = other.transform.GetComponent<PlayerMovement>();
				pm.AddScriptRotation(rotationDiff);
				//other.transform.Rotate(Vector3.up, rotationDiff);
				other.transform.position = receiver.position + positionOffset;
				other.gameObject.GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, rotationDiff, 0) * other.gameObject.GetComponent<Rigidbody>().velocity;
			}
		}
	}
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}