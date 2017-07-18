using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour {

    public GameObject[] asset;
    public BezierCurve bezierCurve;
    public OtherCarsAndScoreAssetManager otherCarManager;
    public UpdateAssetOnTheRoad barriereTravaux;
    public float currentSpeed;
    public Vector2 roadDirection;
    public float timerPopAsset;
    public bool useRandomTimerPopAsset;
    public bool useRandomPosition;
    public bool useRandomAsset;
    public bool useRandomEvent;
    public bool useActivateChildFunction;
    public bool activatePopAsset;
    public float targetTimer;
    public float chanceToSpawnRight;
    public float directionFactor;
    public int deltaBetweenChild;
    private int currentIdChild;
    // Use this for initialization
    void Start () {
        currentIdChild = 0;
        if (useRandomTimerPopAsset)
        targetTimer = Random.Range(0.0f, 5.0f);

        roadDirection = bezierCurve.GetPointAt(1.0f) - bezierCurve.GetPointAt(0.0f);

        StartCoroutine(UpdateInstanciateAsset());
    }

    IEnumerator UpdateInstanciateAsset()
    {
        while (true)
        {
            if (activatePopAsset)
            {
                
                if (useRandomTimerPopAsset)
                    yield return new WaitForSeconds(Random.Range(0.0f, timerPopAsset));
                else
                    yield return new WaitForSeconds(targetTimer);

                float chanceLeft = Random.Range(0.0f, 100.0f);

                if (chanceLeft > chanceToSpawnRight)
                {
                    if (useRandomEvent)
                    {
                        otherCarManager.AvailablePathEvent = 1;
                    }
                    if (useRandomAsset)
                    InstantiateAssetOnLeft(true, Random.Range(0, asset.Length));
                    else
                    InstantiateAssetOnLeft(true, 0);
                }
                else
                {
                    if(useRandomEvent)
                    {
                        otherCarManager.AvailablePathEvent = 2;
                    }

                    if (useRandomAsset)
                        InstantiateAssetOnLeft(false, Random.Range(0, asset.Length));
                    else
                    {
                        if(asset.Length>1)
                        InstantiateAssetOnLeft(false, 1);
                        else
                        InstantiateAssetOnLeft(false, 0);
                        
                    }
                }            
            }
            yield return null;
        }
    }

    void InstantiateAssetOnLeft(bool isLeft, int id)
    {
        GameObject tempAsset = null;
        tempAsset = Instantiate(asset[id]);
        if (isLeft)
        {
            if (useRandomPosition)
                tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = -directionFactor - Random.Range(0.0f, 2.0f);
            else
                tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = -directionFactor;
        }
        else
        {
            if (useRandomPosition)
                tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = directionFactor + Random.Range(0.0f, 2.0f);
            else
                tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = directionFactor;
        }

        if (useActivateChildFunction)
        {
            currentIdChild++;

            if (currentIdChild > deltaBetweenChild)
            {
                tempAsset.GetComponentInChildren<Plot>().GetComponent<Renderer>().enabled = true;
                currentIdChild = 0;
            }
        }

        tempAsset.transform.SetParent(transform);

    }

}
