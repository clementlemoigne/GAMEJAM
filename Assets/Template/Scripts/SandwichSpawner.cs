using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichSpawner : MonoBehaviour 
{
    public GameObject sandwich;

    public Transform leftMarker;
    public Transform rightMarker;

	private const float bigScaleSize = 1.3f;

	private float leftX;
	private float leftY;

	private float rightX;
	private float rightY;

	// Use this for initialization
	void Start () 
    {
        leftX = leftMarker.position.x;
        leftY = leftMarker.position.y;

        rightX = rightMarker.position.x;
        rightY = rightMarker.position.y;

        //Spawn des sandwichs en continue
		InvokeRepeating("InvokeSandwichSpawn", 1, 1);
	}
	
    //délai de spawn d'un sandwich
   void InvokeSandwichSpawn()
    {
        Invoke("SpawnSandwich",Random.Range(0, 0.8f));
    }

	void SpawnSandwich()
    {
        //spawn au dessus de l'écran sur une longueur aléatoire
        Vector3 spawnPosition = new Vector3(Random.Range(leftX, rightX), Random.Range(leftY, rightY), sandwich.transform.position.z);
        GameObject instance = Instantiate(sandwich,spawnPosition, sandwich.transform.rotation);

        //1 sandwich sur 10 rapporte plus de points
        if (Random.Range(0, 10) == 1)
        {
            instance.transform.localScale = new Vector3(bigScaleSize,bigScaleSize,bigScaleSize);
        }

        Destroy(instance,5);
    }

    public float getBigScaleSize()
    {
        return bigScaleSize;
    }
}
