using System;
using System.Collections;
using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
	public GameObject cam;
	public GameObject prefab;
	public int MaxHealth = 100;
	public float speed;
	public float sensitivity;
	public float clampAngle;
	public float knockback;
	float rotX = 0f;
	float rotY = 0f;
	public float jumpForce;
	List<Collider> col = new List<Collider>();
	bool IsGrounded
	{
		get
		{
			return col.Count > 0;
		}
	}

	public void SetSensitivity(float newValue)
	{
		sensitivity = newValue;
	}
	public void AddScriptRotation(float roty)
	{
		rotY += roty;
		Debug.Log((roty, rotY));
	}
	void FixedUpdate()
	{
		if (hasAuthority && !(GameObject.Find("Playernums2").GetComponent<ScoreScript>().localstop))
		{
			if (IsGrounded & Input.GetKey("space"))
			{
				gameObject.GetComponent<Rigidbody>().velocity = (Vector3.up * jumpForce);
				foreach (Collider obj in col)
				{
					Rigidbody rg = obj.gameObject.GetComponent<Rigidbody>();
					if (rg)
					{
						rg.velocity = (Vector3.down * jumpForce * knockback);
					}
				}
				//Debug.Log (gameObject.GetComponent<Rigidbody> ().velocity.y);
			}
			transform.Translate(Vector3.ClampMagnitude(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")), 1) * Time.deltaTime * speed);
			rotY += Input.GetAxis("Mouse X") * Time.fixedDeltaTime * sensitivity;
			rotX -= Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * sensitivity;
			rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
			cam.transform.localRotation = Quaternion.Euler(rotX, 0f, 0.0f);
			transform.rotation = Quaternion.Euler(0f, rotY, 0.0f);
			/*rotY = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * sensitivity;
			rotX = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * sensitivity;
			rotX = Mathf.Clamp(-rotX, -clampAngle, clampAngle);
			cam.transform.Rotate(rotX, 0f , 0.0f);
			transform.Rotate(0f, rotY , 0.0f);*/

			this.PlPosSend(this.name, this.transform.position, this.transform.rotation);
		}

	}
	void OnCollisionStay(Collision coll)
	{

		// Debug.Log (col);   
		if (!col.Contains(coll.collider))
		{
			foreach (var p in coll.contacts)
			{
				//Debug.Log (p.point.y + "|" + GetComponent<Collider2D> ().bounds.min.y);
				if (Mathf.Abs(p.point.y - GetComponent<Collider>().bounds.min.y) < 0.05)
				{
					col.Add(coll.collider);
					break;
					//Debug.Log ("added" + p.collider.gameObject.name);
				}
			}
		}
	}
	void OnCollisionExit(Collision coll)
	{
		//Debug.Log (coll.collider.gameObject.name);
		col.Remove(coll.collider);
		//Debug.Log ("ex" + col);
	}
	// Use this for initialization
	void Start()
	{
		if (PlayerPrefs.HasKey("sensitivity"))
		{
			sensitivity = PlayerPrefs.GetFloat("sensitivity");
		}
	}

	public bool issrv()
	{
		return isServer;
	}
	//..........................................
	public IEnumerator ImmortalityTimer()
	{
		yield return new WaitForSeconds(5f);
		this.gameObject.layer = 12;
		layer(12);
	}
	public void strtImmortalityTimer() { StartCoroutine(ImmortalityTimer()); }

	[ClientRpc]
	public void RcpKillPos(string nam, Vector3 Pos)
	{
		GameObject.Find(nam).transform.position = Pos;
	}

	public IEnumerator kill()
	{
		yield return new WaitForSeconds(3f);
		if (Int32.Parse(this.name[6].ToString()) == GameObject.Find("Camera1").GetComponent<FollowPlayer>().num)
		{

			GameObject.Find("Camera1").GetComponent<FollowPlayer>().OffRed();
		}
		this.gameObject.GetComponent<MeshRenderer>().enabled = true;
		RcpMathof(true);
		Vector3[] arr = GameObject.Find("Camera1").GetComponent<FollowPlayer>().spowns;
		Vector3 pos;
		if (arr.Length == 0)
		{
			pos = new Vector3(0, 0, 0);
			Debug.LogError("No spawn point you must add it in Camera1/FollowPlayer.cs");
		}
		else
		{
			pos = arr[UnityEngine.Random.Range(0, arr.Length)];
		}
		this.transform.position = pos;
		RcpKillPos(this.name, pos);
		this.transform.rotation = Quaternion.identity;
		this.GetComponent<PlayerMovement>().StartCoroutine(ImmortalityTimer());


	}


	//......................................................................................
	[ClientRpc]
	public void RcpUpdatePos(Vector3 p, Quaternion r)
	{
		if (GameObject.Find("Camera1").GetComponent<FollowPlayer>().player.name != this.name)
		{
			this.transform.position = p;
		}
	}
	[Server]
	public void ChgPosPl(string n, Vector3 p, Quaternion r)
	{
		GameObject.Find(n).transform.position = p;
		GameObject.Find(n).transform.rotation = r;
		RcpUpdatePos(p, r);

	}

	[Command]
	public void Ask1(string n, Vector3 p, Quaternion r)
	{
		PlPosSend(n, p, r);

	}


	public void PlPosSend(string n, Vector3 p, Quaternion r)
	{
		if (isServer)
		{
			ChgPosPl(n, p, r);
		}
		else
		{
			Ask1(n, p, r);
		}

	}


	//..................................................................................................................................



	[Server]
	public void startcaruntin(string n)
	{

		GameObject.Find(n).layer = 13;
		GameObject.Find(n).GetComponent<PlayerMovement>().layer(13);
		GameObject.Find(n).GetComponent<PlayerMovement>().StartCoroutine(ImmortalityTimer());  // дать неуяз

	}

	[Command]
	public void Askstartcaruntin(string n)
	{
		StartCaruntin(n);

	}


	public void StartCaruntin(string n)
	{
		if (isServer)
		{
			startcaruntin(n);
		}
		else
		{
			Askstartcaruntin(n);
		}

	}

	//......................................................................
	[ClientRpc]
	public void RcpMathof(bool o)
	{
		this.gameObject.GetComponent<MeshRenderer>().enabled = o;
	}
	[Server]
  public void TakeDamage1(int n, int num)
  {
    //Debug.LogWarning(n.ToString());
    if (this.GetComponent<HealthController>().health + n <= 0)
    {
			Debug.LogWarning((MaxHealth - this.GetComponent<HealthController>().health).ToString());
	  Rpchealth100(MaxHealth);
      if (Int32.Parse(this.name[6].ToString()) == GameObject.Find("Camera1").GetComponent<FollowPlayer>().num)
      {

        GameObject.Find("Camera1").GetComponent<FollowPlayer>().OnRed();
      }
      StartCoroutine(kill());
      layer(13);
      this.gameObject.GetComponent<MeshRenderer>().enabled = false;
      RcpMathof(false);
      if (num != -1){
      GameObject.Find("Playernums(Clone)").GetComponent<NetNums>().AddScore(num);
      }
    }
    else
    {
      Rpcchangehealth(n);
    }

  }

	[Command]
	public void TakeDamage2(int n, int num)
	{
		TakeDamage3(n, num);

	}


	public void TakeDamage3(int n, int num)
	{
		if (isServer)
		{
			TakeDamage1(n, num);
		}
		else
		{
			TakeDamage2(n, num);
		}

	}


	[ClientRpc]
	public void Rpcchangehealth(int n)
	{
		this.GetComponent<HealthController>().health += n;
	}


	[ClientRpc]
	public void Rpchealth100(int n)
	{
		this.GetComponent<HealthController>().health = n;
	}

	//.................................................................

	public void layer2(int a) {
		layer(a);
		Debug.LogWarning("ggggggggggggggg");
	}


	[ClientRpc]
	public void layer(int l)
	{
		this.gameObject.layer = l;
	}
	//..................................................................
	[Server]
	public void spwnbul1(Vector3 Pos, Quaternion Rot, int num)
	{
		GameObject bul = Instantiate(prefab, Pos, Rot);
		NetworkServer.Spawn(bul);
		bul.GetComponent<BulletScript>().num = num;

	}
	[Command]
	public void spwnbul2(Vector3 Pos, Quaternion Rot, int num)
	{
		spwnbul3(Pos, Rot, num);
	}



	public void spwnbul3(Vector3 Pos, Quaternion Rot, int num)
	{
		if (isServer)
		{
			spwnbul1(Pos, Rot, num);
		}
		else
		{
			spwnbul2(Pos, Rot, num);
		}
	}





}
