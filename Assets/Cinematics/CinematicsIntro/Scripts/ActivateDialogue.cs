using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour {

    public GameObject dialogue;

    public void ActiveGameObject()
    {
        dialogue.SetActive(true);
    }
}
