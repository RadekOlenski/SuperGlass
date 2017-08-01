using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChanger : MonoBehaviour
{
    private Vector3 gravity;
    private float gravityMultiplier = 9.81f;
	void FixedUpdate ()
	{
	    gravity = Input.acceleration;
		Physics.gravity = new Vector3(gravity.x, gravity.y) * gravityMultiplier;
	}
}
