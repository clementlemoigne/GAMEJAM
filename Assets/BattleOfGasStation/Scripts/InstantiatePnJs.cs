using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePnJs : MonoBehaviour {

    public GameObject[] pnjs;
    public float minTimerPop;
    public float maxTimerPop;
    public Animator door;
    // Use this for initialization
    void Start () {
        StartCoroutine(IntantiateRandomPnJ());
	}

    IEnumerator IntantiateRandomPnJ()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimerPop, maxTimerPop));
            GameObject temp = Instantiate(pnjs[Random.Range(0, pnjs.Length)],transform.position,transform.rotation);
            temp.transform.SetParent(temp.transform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ennemy")
        {
            door.SetBool("Open", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ennemy")
        {
            door.SetBool("Open", false);
        }
    }

}
