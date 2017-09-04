using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script gère les instanciations des véhicules, bonus et obstacles sur la route.
/// </summary>
public class OtherCarsAndScoreAssetManager : MonoBehaviour {

    public GameObject scoreAsset;

    public Texture2D front;
    public Texture2D back;
    

    public BezierCurve bezierCurve;
    public float currentSpeed;
    public Vector2 roadDirection;
    public float timer;
    public float targetTimer;

    public float minTargetTimer;
    public float maxTargetTimer;
    private int maxCarInHorizon;
    public int maxCarOnNormalEvent;
    public int maxCarOnSpecialEvent;
    public int AvailablePathEvent;

    public float ChanceUseSwapFunction;
    public float ChanceAsset;
    public GameObject[] cars;
    public List<int> availablePath = new List<int>();
    public bool canInstantiate;
    public bool canInstantiateEvent;


    void Start () {
        canInstantiate = true;
        roadDirection = bezierCurve.GetPointAt(1.0f) - bezierCurve.GetPointAt(0.0f);
        

        StartCoroutine(UpdateInstantiateAssets());
    }


    IEnumerator UpdateInstantiateAssets()
    {

        yield return new WaitForSeconds(targetTimer);
        while (canInstantiate)
        {
            switch (AvailablePathEvent)
            {
                case 0:
                    {
                        maxCarInHorizon = maxCarOnNormalEvent;
                        availablePath.Add(0);
                        availablePath.Add(1);
                        availablePath.Add(2);
                        availablePath.Add(3);
                        break;
                    }
                case 1:
                    {
                        maxCarInHorizon = maxCarOnSpecialEvent;
                        availablePath.Add(1);
                        availablePath.Add(2);
                        availablePath.Add(3);
                        
                        break;
                    }
                case 2:
                    {
                        maxCarInHorizon = maxCarOnSpecialEvent;
                        availablePath.Add(0);
                        availablePath.Add(1);
                        availablePath.Add(2);
                        break;
                    }
            }
            canInstantiateEvent = true;
            yield return new WaitForSeconds(Random.Range(minTargetTimer, maxTargetTimer));
            canInstantiateEvent = false;
            //for(int i = 0; i < availablePath.Capacity; i++ )
            //{
            int RandomNBCar = Random.Range(1, maxCarInHorizon+1);



            //Debug.Log("Instantiate Car on ");
            for (int i = 0; i < RandomNBCar; i++)
            {
                //Debug.Log("availablePath Count Car -> " + availablePath.Count);
                int idRoad = Random.Range(0, availablePath.Count);
                //Debug.Log("idRoad -> " + availablePath[idRoad]);

                InstantiateCarAsset(availablePath[idRoad], availablePath);
            }

            //Debug.Log("availablePath Count Note-> " + availablePath.Count);
            int idNoteRoad = Random.Range(0, availablePath.Count);
            //Debug.Log("idNoteRoad -> " + availablePath[idNoteRoad]);

            InstantiateNoteAsset(availablePath[idNoteRoad]);

            //Debug.Log("AfterNoteInstantiate");

            availablePath.Clear();
        }

        yield break;
    }

    void InstantiateNoteAsset(int id_road)
    {
        float currentChanceAsset = Random.Range(0.0f, 100.0f);
        if (currentChanceAsset < ChanceAsset)
        {
            GameObject tempNote = null;
            tempNote = Instantiate(scoreAsset);
            tempNote.GetComponent<UpdateAssetOnTheRoad>().canUseSwapFunction = false;
            tempNote.GetComponent<UpdateAssetOnTheRoad>().idCurrentPath = -1;

            //int idRoadScore = availablePath[Random.Range(0, 2)];

            Debug.Log(id_road + " ");
            
            switch (id_road)
            {
                case 0:
                    {
                        tempNote.GetComponent<UpdateAssetOnTheRoad>().directionFactor = -1.1f;
                        tempNote.GetComponent<UpdateAssetOnTheRoad>().speedFactor = 0.5f;
                        break;
                    }
                case 1:
                    {
                        tempNote.GetComponent<UpdateAssetOnTheRoad>().directionFactor = -0.3f;
                        tempNote.GetComponent<UpdateAssetOnTheRoad>().speedFactor = 0.5f;
                        break;
                    }
                case 2:
                    {
                        tempNote.GetComponent<UpdateAssetOnTheRoad>().directionFactor = 0.5f;
                        tempNote.GetComponent<UpdateAssetOnTheRoad>().speedFactor = 0.5f;
                        break;
                    }

                case 3:
                    {
                        tempNote.GetComponent<UpdateAssetOnTheRoad>().directionFactor = 1.1f;
                        tempNote.GetComponent<UpdateAssetOnTheRoad>().speedFactor = 0.5f;

                        break;
                    }
            }
            tempNote.GetComponent<UpdateAssetOnTheRoad>().speedDivide = 90.0f;
            tempNote.transform.SetParent(transform);
            availablePath.Remove(id_road);
        }
    }

    void InstantiateCarAsset(int id_road, List<int> availablePath)
    {
        GameObject tempAsset = null;

        tempAsset = Instantiate(cars[Random.Range(0, cars.Length)]);
        
        Vector3 tempLocalScale = tempAsset.GetComponentInChildren<Renderer>().transform.localScale;

        //float currentChanceUseSwapFunction = Random.Range(0.0f, 100.0f);

        /*if (currentChanceUseSwapFunction > ChanceUseSwapFunction) 
        {
            tempAsset.GetComponent<UpdateAssetOnTheRoad>().canUseSwapFunction = true;
        }
        else
        {
            tempAsset.GetComponent<UpdateAssetOnTheRoad>().canUseSwapFunction = false;
        }*/
        tempAsset.GetComponent<UpdateAssetOnTheRoad>().canUseSwapFunction = false;
        tempAsset.GetComponent<UpdateAssetOnTheRoad>().availablePaths = availablePath;
        switch (id_road)
        {
            case 0:
                {
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().idCurrentPath = 0;
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = -1.1f;
                    tempAsset.GetComponentInChildren<SpriteRenderer>().sprite = tempAsset.GetComponent<UpdateAssetOnTheRoad>().swapAsset[0];
                    tempAsset.GetComponentInChildren<Renderer>().gameObject.tag = "backcar";
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().speedFactor = 0.5f;
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().speedDivide = 90.0f;
                    break;
                }
            case 1:
                {
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().idCurrentPath = 1;
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = -0.3f;
                    tempAsset.GetComponentInChildren<SpriteRenderer>().sprite = tempAsset.GetComponent<UpdateAssetOnTheRoad>().swapAsset[1];
                    tempAsset.GetComponentInChildren<Renderer>().gameObject.tag = "backcar";
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().speedFactor = 0.5f;
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().speedDivide = 90.0f;
                    break;
                }
               
            case 2:
                {
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().idCurrentPath = 2;
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = 0.5f;
                    tempAsset.GetComponentInChildren<SpriteRenderer>().sprite = tempAsset.GetComponent<UpdateAssetOnTheRoad>().swapAsset[2];
                    tempAsset.GetComponentInChildren<Renderer>().gameObject.tag = "backcar";
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().speedFactor = 0.5f;
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().speedDivide = 90.0f;
                    break;
                }

            case 3:
                {
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().idCurrentPath = 3;
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = 1.1f;
                    tempAsset.GetComponentInChildren<SpriteRenderer>().sprite = tempAsset.GetComponent<UpdateAssetOnTheRoad>().swapAsset[3];
                    tempAsset.GetComponentInChildren<Renderer>().gameObject.tag = "backcar";
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().speedFactor = 0.5f;
                    tempAsset.GetComponent<UpdateAssetOnTheRoad>().speedDivide = 90.0f;
                    break;
                }
        }

        availablePath.Remove(id_road);
        
    }
}
