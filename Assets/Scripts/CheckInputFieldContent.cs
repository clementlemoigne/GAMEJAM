using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInputFieldContent : MonoBehaviour {

    public GameObject next;
    public InputField inputField;
	// Use this for initialization
	void Start () {
		
	}
	
    public void UpdateField()
    {
        string text = inputField.text;
        text = text.Replace(" ", "");
        inputField.text = text;
    }

	// Update is called once per frame
	void Update () {

        if (Input.anyKeyDown)
        {
            SoundManager.instance.LoadSound("Pickup_", 0.7f, Random.Range(0.5f,1.5f));
        }

        if(!string.IsNullOrEmpty(inputField.text))
        {
            next.SetActive(true);
        }
        else
            next.SetActive(false);

    }
}
