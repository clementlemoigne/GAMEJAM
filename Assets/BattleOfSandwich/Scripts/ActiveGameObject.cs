using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameObject : MonoBehaviour {

    public GameObject myGameobject;

    void ActivateGameobject()
    {
        if(myGameobject != null)
        myGameobject.SetActive(true);
    }

}
