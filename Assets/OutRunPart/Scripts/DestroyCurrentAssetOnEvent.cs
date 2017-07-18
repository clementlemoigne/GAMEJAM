using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCurrentAssetOnEvent : MonoBehaviour {
	
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "asset")
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
