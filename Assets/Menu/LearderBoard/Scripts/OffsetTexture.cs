using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetTexture : MonoBehaviour {

    public Material material;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        material.mainTextureOffset += Vector2.up * speed * Time.deltaTime;

    }
}
