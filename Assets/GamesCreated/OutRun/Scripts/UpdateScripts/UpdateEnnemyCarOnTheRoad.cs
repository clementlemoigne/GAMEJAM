using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateEnnemyCarOnTheRoad : MonoBehaviour {

    public float turnFactor;
    public Transform car;
    public DelegateHandler delegateHandler;
    public BezierCurve bezierCurve;
    public Text EnnemyHealth;
    public Animator EnnemyBar;
    public PlayerStatManager playerStat;
    public InputManagerOutRunPart inputManager;
    public OtherCarsAndScoreAssetManager otherCarsAsset;
    public GameObject CD;
    public GameObject explosion;
    public Texture2D[] shapes;
    public Texture2D[] cdShapes;
    public GameObject[] smokes;
    public PlayerTriggerManager playerTriggerManager;
    public GameObject PopUpScore;
    private Renderer EnnemyRenderer;
    private Rigidbody ennemyRigidbody;
    [Range(0.0f, 1.0f)]
    public float currentPosition;
    public float directionFactor;
    public float deceleratefactor;
    public float OffsetY;
    public float scaleOffset;
    public float minDistanceForCds;
    public float minDistanceOnTheRoad;
    public float minTimeCD;
    public float maxTimeCD;
    public LerpScore ScoreNiv;
    public bool activeDecelerate;
    private bool canDecelerate;
    public float decelerationWhenInvinsibleFactor;
    public float decelerationWhenNotInvinsibleFactor;

    public bool activeAccelerate;
    public bool canLaunchCd;
    public bool canswap;
    public bool canUpdate;
    public bool stopCurrentAction;
    public int idCurrentPath;
    public int maxPath;
    public int minPath;
    public float[] directionfactors;
    public int ennemyHealth;

    // Use this for initialization
    void Start () {

        minPath = 0;
        maxPath = 3;
        EnnemyRenderer = GetComponentInChildren<Renderer>();
        ennemyRigidbody = GetComponent<Rigidbody>();
        ennemyHealth = 100;
        canswap = true;
        canLaunchCd = true;
        canDecelerate = false;
        idCurrentPath = 3;
        currentPosition = 0.15f;
        deceleratefactor = decelerationWhenNotInvinsibleFactor;

        smokes[0].SetActive(false);
        smokes[1].SetActive(false);
        smokes[2].SetActive(false);
        smokes[3].SetActive(true);

        StartCoroutine(ChangePositionAt(0.9f, 1.0f));
        StartCoroutine(CheckIsTuto());
        StartCoroutine(UpdateEnnemyCarOnRoad());
    }

    IEnumerator CheckIsTuto()
    {
        while (inputManager.isTuto)
        {
            yield return null;
        }

        canDecelerate = true;
    }

    IEnumerator UpdateCDOnRoad()
    {

        yield return new WaitForSeconds(Random.Range(minTimeCD, maxTimeCD));

        GameObject cd = Instantiate(CD);
        UpdateAssetOnTheRoad updateAssetOnTHeRoad = cd.GetComponent<UpdateAssetOnTheRoad>();

        cd.GetComponentInChildren<Renderer>().material.mainTexture = cdShapes[idCurrentPath];
        updateAssetOnTHeRoad.canUseSwapFunction = false;
        updateAssetOnTHeRoad.currentPosition = 1.0f;
        updateAssetOnTHeRoad.directionFactor = directionFactor;
        updateAssetOnTHeRoad.speedFactor = 0.5f;
        //canLaunchCd = true;
        yield break;
    }

    IEnumerator UpdateEnnemyCarOnRoad()
    {
        // update position on road
        canUpdate = true;
        bool isEnd = false;
        while (canUpdate)
        {
            ennemyRigidbody.AddForce(Vector3.up * 0.00001f);
            if (ennemyHealth <= 0 && isEnd == false)
            {
                isEnd = true;
                ScoreNiv.targetNumber = playerStat.score;
                SoundManager.instance.LoadMusic("F1 Havas Victory Sun", 1.0f);
                //StartCoroutine(DesactivateUpdate(2.0f));
                playerStat.EndOfLevel();
                PopUpScore.SetActive(true);
                yield return null;
            }

            if (currentPosition < 1.0f)
            {
                transform.position = bezierCurve.GetPointAt(currentPosition) + Vector3.right * directionFactor * 3.0f * (1.0f - currentPosition) + Vector3.up * OffsetY + Vector3.up * (1.0f - currentPosition);
                transform.localScale = Vector3.one * (scaleOffset - (currentPosition / scaleOffset));

                if (activeDecelerate && canDecelerate && !isEnd)
                {
                    currentPosition -= Time.deltaTime * deceleratefactor;
                }
                else if (activeAccelerate && !isEnd)
                {
                    activeAccelerate = false;
                    StartCoroutine(ChangePositionAt(currentPosition + 0.2f, 1.0f));
                }

                if (currentPosition < minDistanceOnTheRoad)
                {
                    currentPosition = minDistanceOnTheRoad;
                    activeDecelerate = false;
                }

                if(currentPosition > minDistanceForCds && canLaunchCd && !activeDecelerate && canDecelerate && !isEnd)
                {
                    canLaunchCd = false;
                    StartCoroutine(UpdateCDOnRoad());
                }

            }

            yield return transform;

        }

    }

    IEnumerator DesactivateUpdate(float timer)
    {
        yield return new WaitForSeconds(timer);
        canUpdate = false;
    }

    public void CheckVehicleOnFront()
    {
        

        if (idCurrentPath < maxPath && idCurrentPath > minPath)
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

            Debug.Log("In Travaux");

            if (idCurrentPath == 1 || idCurrentPath == 2)
            {
                switch (otherCarsAsset.AvailablePathEvent)
                {
                    case 0:
                        {
                            maxPath = 3;
                            minPath = 0;
                            break;
                        }
                    case 1:
                        {
                            maxPath = 3;
                            minPath = 1;
                            break;
                        }
                    case 2:
                        {
                            maxPath = 2;
                            minPath = 0;
                            break;
                        }
                }
            }
        }
        else
        {
            if(idCurrentPath == maxPath)
            {
                idCurrentPath = maxPath-1;
            }
            else if(idCurrentPath == minPath)
            {
                idCurrentPath = minPath+1;
            }

            else if(idCurrentPath > maxPath)
            {
                idCurrentPath = maxPath;
            }
            else if (idCurrentPath < minPath)
            {
                idCurrentPath = minPath;
            }

        }
        canswap = false;

        Debug.Log("Move To -> " + idCurrentPath);
        MoveCar(directionfactors[idCurrentPath]);
    }       

    IEnumerator ChangePositionAt(float target, float speed)
    {
        yield return new WaitForSeconds(0.2f);

        stopCurrentAction = false;
        if (target > 0.9f)
        {
            target = 0.9f;
        }

        while (currentPosition < target - 0.03f)
        {
            currentPosition = Mathf.Lerp(currentPosition, target, Time.deltaTime* speed);

            if(stopCurrentAction)
            {
                target = currentPosition;

                if (playerTriggerManager.isInvinsible)
                    deceleratefactor = decelerationWhenInvinsibleFactor;
                else
                    deceleratefactor = decelerationWhenNotInvinsibleFactor;

            }

            yield return currentPosition; 
        }

        if (currentPosition > 0.9f)
        {
            currentPosition = 0.9f;
        }
        else
        {
            currentPosition = target;
        }  

        activeDecelerate = true;
        yield break;
    }

    void OnTriggerEnter(Collider other)
    {
        if((other.tag == "frontcar" || other.tag == "backcar" || other.tag == "asset") && canswap)
        {
            Debug.Log("CheckVehicleOnFront");
            if(other.transform.parent != null)
            if (other.transform.parent.GetComponent<UpdateAssetOnTheRoad>().availablePaths != null)
            {
                    if (other.transform.parent.GetComponent<UpdateAssetOnTheRoad>().availablePaths.Count > 0)
                    {
                        maxPath = other.transform.parent.GetComponent<UpdateAssetOnTheRoad>().availablePaths[other.transform.parent.GetComponent<UpdateAssetOnTheRoad>().availablePaths.Count - 1];
                        minPath = other.transform.parent.GetComponent<UpdateAssetOnTheRoad>().availablePaths[0];
                    }
            }
            CheckVehicleOnFront();
         
        }

        /*if(other.tag == "asset")
        {
            //SoundManager.instance.LoadSound("chock1", 0.7f, 1.0f);
            GameObject tempExplosion = Instantiate(explosion, car.transform.position, Quaternion.identity);
        }*/
    }

    public void GoToHorizon(Transform player)
    {
        SoundManager.instance.LoadSound("chock1", 0.7f, 1.0f);
        stopCurrentAction = true;

        PlayerTriggerManager playerTriggerManager = player.GetComponent<PlayerTriggerManager>();
        PlayerStatManager playerStatManager = player.GetComponent<PlayerStatManager>();

        if (playerTriggerManager.canColide)
        {
            ennemyHealth -= playerStatManager.damage;
        }
        else
        {
            ennemyHealth -= playerStatManager.damage * 2;
        }

        EnnemyBar.Play("EnnemyLife", 0, ennemyHealth / 100.0f);
        EnnemyBar.speed = 0.0f;
        activeDecelerate = false;
        StartCoroutine(ChangePositionAt(0.9f, 1.0f));

        if (!playerTriggerManager.isInvinsible)
            delegateHandler.OnDamage(40);
    }

    void MoveCar(float targetDirectionFactor)
    {
        canLaunchCd = false;
        StartCoroutine(ChangeTextureIn(0.2f));
        StartCoroutine(LerpPosition(targetDirectionFactor));
    }

    IEnumerator ChangeTextureIn(float timer)
    {
        yield return new WaitForSeconds(timer);
        smokes[0].SetActive(false);
        smokes[1].SetActive(false);
        smokes[2].SetActive(false);
        smokes[3].SetActive(false);

        smokes[idCurrentPath].SetActive(true);

        EnnemyRenderer.material.mainTexture = shapes[idCurrentPath];
    }

    IEnumerator LerpPosition(float targetDirectionFactors)
    {
        while (directionFactor < targetDirectionFactors -0.07f || directionFactor > targetDirectionFactors +0.07f)
        {
            directionFactor = Mathf.Lerp(directionFactor, targetDirectionFactors, Time.deltaTime * turnFactor);
            yield return directionFactor;
        }
        directionFactor = targetDirectionFactors;
        canswap = true;
        canLaunchCd = true;
        yield break;
    }

}
