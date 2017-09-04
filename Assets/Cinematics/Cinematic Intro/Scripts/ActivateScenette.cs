using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScenette : MonoBehaviour {

    public GameObject scenette;

    public void ActiveGameObject()
    {
        scenette.SetActive(true);
    }
}
