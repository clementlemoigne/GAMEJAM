using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCurrentAssetOnEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "asset")
        {
            Debug.Log("Destroy Asset Because Of Event");
            Destroy(transform.parent.gameObject);
        }
    }
}
