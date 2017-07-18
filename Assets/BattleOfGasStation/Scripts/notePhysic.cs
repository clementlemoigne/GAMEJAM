using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notePhysic : MonoBehaviour {

    public float speed;
    private RectTransform noteRectTransform;
	// Use this for initialization
	void Start () {
        noteRectTransform = transform as RectTransform;
        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown()
    {
        while (true)
        {
            noteRectTransform.Translate(Vector3.down*Time.deltaTime*speed);
            yield return null;
        }
    }

}
