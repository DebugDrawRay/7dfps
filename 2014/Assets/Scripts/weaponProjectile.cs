using UnityEngine;
using System.Collections;

public class weaponProjectile : MonoBehaviour 
{
	public float projectileSpeed;

	void Start () 
	{
		rigidbody.velocity = transform.forward * projectileSpeed;
	}

}
