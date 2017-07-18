using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRotationWithTransform : MonoBehaviour {

    public Transform bezierPoint;
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.Euler(15.0f, 0.0f, bezierPoint.position.x); 

	}
}
