using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameObject : MonoBehaviour {

    public GameObject myGameobject;

    void ActivateGameobject()
    {
        Debug.Log("Activate GameObject");
        if(myGameobject != null)
        myGameobject.SetActive(true);
    }

}
