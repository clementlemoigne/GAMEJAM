using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAssetOnTheRoad : MonoBehaviour
{
    public float turnFactor;
    private BezierCurve bezierCurve;
    private UpdateEnnemyCarOnTheRoad ennemyCar;

    public float directionFactor;
    public float speedFactor;
    public float scaleOffset;
    public float currentPosition;
    public float OffsetY;
    public float speedDivide;

    public bool canswap;
    public bool canUseSwapFunction;
    public bool canCheckEvent;
    public Sprite[] swapAsset;
    public int idCurrentPath;
    public float[] directionfactors;
    public List<int> availablePaths = new List<int>();
    // Use this for initialization
    void Start()
    {
        //currentPosition = 1.0f;

        ennemyCar = GameObject.FindGameObjectWithTag("ennemy").GetComponent<UpdateEnnemyCarOnTheRoad>();

        
        bezierCurve = GameObject.FindGameObjectWithTag("bezierRoad").GetComponent<BezierCurve>();
        
        DelegateHandler.ChangeGlobalspeed += ChangeSpeed;

        if(canUseSwapFunction)
        {
            CheckVehicleTurn();
        }

        StartCoroutine(UpdateRoadPartSpeed());

    }

    public void CheckVehicleTurn()
    {
        // update colision with vehicles

        if (idCurrentPath < 3 && idCurrentPath > 0)
        {
            float chanceLeft = Random.Range(0.0f, 100.0f);
            if (chanceLeft < 50.0f)
            {
                idCurrentPath--;
            }
            else
            {
                idCurrentPath++;
            }
        }
        else
        {
            if (idCurrentPath == 3)
            {
                idCurrentPath = 2;
            }
            else if (idCurrentPath == 0)
            {
                idCurrentPath = 1;
            }
        }
        canswap = false;
        MoveCar(directionfactors[idCurrentPath], 0.0f);
    }

    void MoveCar(float targetDirectionFactor, float atTime)
    {
        StartCoroutine(LerpPosition(targetDirectionFactor, atTime));
    }

    IEnumerator LerpPosition(float targetDirectionFactors,float atTime)
    {
        yield return new WaitForSeconds(atTime);

        StartCoroutine(ChangeTexture(0.5f));

        while (directionFactor < targetDirectionFactors - 0.02f || directionFactor > targetDirectionFactors + 0.02f)
        {
            directionFactor = Mathf.Lerp(directionFactor, targetDirectionFactors, Time.deltaTime * turnFactor);
            yield return directionFactor;
        }  

        directionFactor = targetDirectionFactors;
        canswap = true;
        yield break;
    }

    IEnumerator ChangeTexture(float atTimer)
    {
        yield return new WaitForSeconds(atTimer);
        GetComponentInChildren<SpriteRenderer>().sprite = swapAsset[idCurrentPath];
        yield break;
    }

    IEnumerator UpdateRoadPartSpeed()
    {
        while (true)
        {
            currentPosition -= Time.deltaTime * speedFactor;
            transform.position = bezierCurve.GetPointAt(currentPosition) + Vector3.right * directionFactor * 3f * (1.0f - currentPosition) + Vector3.up * OffsetY + Vector3.up * (1.0f - currentPosition);
            transform.localScale = Vector3.one * (scaleOffset - (currentPosition / scaleOffset));

            /*if (canCheckEvent)
            {
                if (idCurrentPath < ennemyCar.minPath || idCurrentPath > ennemyCar.maxPath)
                {
                    Destroy(gameObject);
                }
            }*/

            if (transform.position.z == 0.0f )
            {
                Destroy(gameObject);
            }

            yield return transform;
        }
    }

    public void ChangeSpeed(float value)
    {
        speedFactor = value/ speedDivide;
    }

    void OnDestroy()
    {
        DelegateHandler.ChangeGlobalspeed -= ChangeSpeed;
    }
}


