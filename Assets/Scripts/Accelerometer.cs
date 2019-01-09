using UnityEngine;
using System.Collections;

public class Accelerometer : MonoBehaviour {
	
	float xRot, yRot, zRot;
	public float rotSpeed = 20f;
	
	void Update ()
	{
		// This tilts the axis of the camera like shaking a head yes
		xRot = Input.acceleration.z * -90f;
		// This tilts like a driving wheel to make it like shaking head no
		yRot = Input.acceleration.x * 360f;
		zRot = 0f;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(xRot, yRot, zRot)), Time.deltaTime * rotSpeed);
	}
	
}