using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateGameObject : MonoBehaviour {

    public GameObject myObject;


    public void DesactivateMyObject()
    {
        myObject.SetActive(false);
    }
}
