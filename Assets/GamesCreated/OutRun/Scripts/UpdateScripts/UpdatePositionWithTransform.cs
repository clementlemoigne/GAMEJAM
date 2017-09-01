using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePositionWithTransform : MonoBehaviour {

    public BezierPoint bezierPoint;
    public Transform HorizonPosition;
    public float sunTurnAttenuation;
    private Vector3 Height;
    private Vector3 Offset;

    // Use this for initialization
    void Start () {
        Height = transform.localPosition;

    }
	
	// Update is called once per frame
	void Update () {

        transform.localPosition = Vector3.up * Height.y - Vector3.right * bezierPoint.localPosition.x * sunTurnAttenuation + Vector3.right* Height.x + Vector3.forward*HorizonPosition.localPosition.z;


    }
}
