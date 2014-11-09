using UnityEngine;
using System.Collections;

public class playerCharacter : MonoBehaviour 
{

	public float moveSpeed;
	public float sprintMod;
	public float jumpMod;
	public float weaponDelay;
	public int ammoCount;

	private float cooldownTime;

	public GameObject projectileType;

	private bool grounded;
	private bool weaponReady;
	private bool hasAmmo;
	private float rotX = 0;
	void Start ()
	{
		weaponReady = true;
		grounded = true;
		cooldownTime = weaponDelay;
		ammoCount = 6;
	}

	void Update()
	{
		actionListener();
		weaponCooldown();
		ammoController();
	}

	//handles all events on user input
	void actionListener()
	{
		mouseController();
		moveAction(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		jumpAction(Input.GetButtonDown("Jump"), jumpMod);
		fireWeapon(Input.GetButtonDown("Fire"));
	}

	void moveAction(float hor, float ver)
	{
		rigidbody.velocity = new Vector3(hor* moveSpeed * sprintAction(), rigidbody.velocity.y, ver * moveSpeed * sprintAction());
	}

	float sprintAction()
	{
		if (Input.GetButtonDown("Sprint"))
		{
			return sprintMod;
		}
		else
		{
			return 1;
		}
	}

	void jumpAction(bool jump, float mod)
	{
		if(jump && grounded)
		{
			rigidbody.AddForce(Vector3.up * mod);
			grounded = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "ground")
		{
			grounded = true;
		}
	}
	void mouseController()
	{ 
		rotX += Input.GetAxis("Mouse X");
		transform.localRotation = Quaternion.AngleAxis (rotX, Vector3.up);
	}

	void fireWeapon (bool fire) 
	{ 
		RaycastHit hit;
		Ray fireRay = new Ray(GameObject.FindGameObjectWithTag("playerWeapon").transform.position, GameObject.FindGameObjectWithTag("playerWeapon").transform.forward);
		if (fire && weaponReady && hasAmmo)
		{ 
			bool castRay = Physics.Raycast(fireRay, out hit);

			if(castRay)
			{
				Vector3 target = hit.point;
				GameObject projectile;
				projectile = Instantiate(projectileType, GameObject.FindGameObjectWithTag("playerWeapon").transform.position, Quaternion.identity) as GameObject;
				projectile.transform.LookAt(target);
			}
			ammoCount --;
			weaponReady = false;
		} 
	} 

	void weaponCooldown()
	{
		if (weaponReady == true)
		{
			cooldownTime = weaponDelay;
		}
		if (weaponReady == false)
		{
			cooldownTime -= Time.deltaTime;
			if (cooldownTime <= 0)
			{
				weaponReady = true;
			}
		}
	}

	void ammoController()
	{
		if(ammoCount > 0)
		{
			hasAmmo = true;
		}
		else
		{
			hasAmmo = false;
		}
	}
}
