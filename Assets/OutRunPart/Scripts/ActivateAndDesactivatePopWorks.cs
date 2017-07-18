using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAndDesactivatePopWorks: MonoBehaviour {

    public bool usefunction;
    public float maxTimer;
    public float minTimer;
    public OtherCarsAndScoreAssetManager otherCarManager;
    public UpdateEnnemyCarOnTheRoad updateEnnemy;
    private AssetManager assetManager;

	// Use this for initialization
	void Start () {
        assetManager = GetComponent<AssetManager>();
        StartCoroutine(ActivateAndDesactivateWithTimer());
    }

    IEnumerator ActivateAndDesactivateWithTimer()
    {
        float timer = 0.0f;
        float timerRandom = Random.Range(minTimer, maxTimer);
        
        while (timer < timerRandom)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        assetManager.activatePopAsset = !assetManager.activatePopAsset;

        if(!assetManager.activatePopAsset)
        {
            otherCarManager.AvailablePathEvent = 0;
        }
        else
        {
            int RandomAvailablePathEvent = Random.Range(1, 3);
            GameObject tempAsset = null;
            tempAsset = Instantiate(assetManager.asset[2]);
            switch (RandomAvailablePathEvent)
            {
                case (1):
                    {
                        tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = -1.1f;
                        tempAsset.GetComponent<UpdateAssetOnTheRoad>().availablePaths.Add(1);
                        tempAsset.GetComponent<UpdateAssetOnTheRoad>().availablePaths.Add(2);
                        tempAsset.GetComponent<UpdateAssetOnTheRoad>().availablePaths.Add(3);
                        assetManager.chanceToSpawnRight = 0.0f;
                        break;
                    }
                case (2):
                    {
                        tempAsset.GetComponent<UpdateAssetOnTheRoad>().directionFactor = 1.1f;
                        tempAsset.GetComponent<UpdateAssetOnTheRoad>().availablePaths.Add(0);
                        tempAsset.GetComponent<UpdateAssetOnTheRoad>().availablePaths.Add(1);
                        tempAsset.GetComponent<UpdateAssetOnTheRoad>().availablePaths.Add(2);
                        assetManager.chanceToSpawnRight = 100.0f;
                        break;
                    }
            }
            otherCarManager.AvailablePathEvent = RandomAvailablePathEvent;
          
        }

        StartCoroutine(ActivateAndDesactivateWithTimer());

        yield break;
    }
}
