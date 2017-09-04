using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePnJs : MonoBehaviour {

    public GameObject[] pnjs;
    public float minTimerPop;
    public float maxTimerPop;
    public Animator Porte;
    // Use this for initialization
    void Start () {
        StartCoroutine(IntantiateRandomPnJ());
	}

    IEnumerator IntantiateRandomPnJ()
    {
        //yield return new WaitForSeconds(5.0f);
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
            Porte.SetBool("Open", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ennemy")
        {
            Debug.Log("Exit");
            Porte.SetBool("Open", false);
        }
    }

}
