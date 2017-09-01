using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCentrifugalEffect : MonoBehaviour {

    public Transform horizonPoint;
    public Rigidbody playerRigidbody;
    public float centrifugalFactoreffect;

	
	// Update is called once per frame
	void Update () {
        UpdateCentrifugalEffect();
    }

    void UpdateCentrifugalEffect()
    {
        if (horizonPoint.transform.localPosition.x == 0.0f)
        {
            Debug.Log("Tout droit");
            //rigidbody.velocity = Vector3.zero;
        }
        else
        {
            playerRigidbody.AddForce(Vector3.left * horizonPoint.transform.localPosition.x * centrifugalFactoreffect);
        }
    }

}
