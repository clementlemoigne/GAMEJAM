using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamieManager : MonoBehaviour 
{
    public GameObject mamie;
    public Transform spawnTransform1;
    public Transform spawnTransform2;

    public int delay = 10;

    private void Start()
    {
        //active et désactive la grand-mere en continue
        InvokeRepeating("EnableDisableMamie", delay, delay);
    }

    void EnableDisableMamie()
    {
        if(mamie.activeInHierarchy)
        {
            mamie.SetActive(false);
        }

        else
        {
            Transform mamieSpawn = spawnTransform1;

            //change de sens d'arrivée aléatoirement
            if(Random.Range(0,2)== 1)
            {
                mamieSpawn = spawnTransform2;
            }

            Vector3 mamiePos = mamie.transform.position;

            mamie.transform.position = new Vector3(mamieSpawn.position.x,mamiePos.y, mamiePos.z);
            mamie.transform.localRotation = mamieSpawn.localRotation;

            mamie.SetActive(true);
        }

    }

}
