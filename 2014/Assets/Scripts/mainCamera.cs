using UnityEngine;
using System.Collections;

public class mainCamera : MonoBehaviour 
{
	private float rotY = 0;
	void Awake()
	{
		Screen.lockCursor = true;
	}
	void Update()
	{
		mouseController();
	}
	void mouseController ()
	{
		rotY += Input.GetAxis("Mouse Y");
		transform.localRotation = Quaternion.AngleAxis (rotY, Vector3.left);
	}
}
