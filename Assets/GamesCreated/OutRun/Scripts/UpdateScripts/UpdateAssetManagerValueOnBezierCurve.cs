using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAssetManagerValueOnBezierCurve : MonoBehaviour {

    public Transform bezierPoint;
    public AssetManager tunnelManager;
    private AssetManager assetManager;
	// Use this for initialization
	void Start () {

        assetManager = GetComponent<AssetManager>();

    }
	
	// Update is called once per frame
	void Update () {

        //if (!tunnelManager.activatePopAsset)
        //{
            if (bezierPoint.localPosition.x > 0.3f)
            {
                assetManager.chanceToSpawnRight = -1.0f;
                assetManager.activatePopAsset = true;

            }
            else if (bezierPoint.localPosition.x < -0.3f)
            {
                assetManager.chanceToSpawnRight = 100.0f;
                assetManager.activatePopAsset = true;
            }
            else
            {
                if (assetManager.activatePopAsset)
                {
                    assetManager.activatePopAsset = false;
                }
            }
       // }
        /*else
        {
            if (assetManager.activatePopAsset)
            {
                assetManager.activatePopAsset = false;
            }
        }  */
	}
}
