using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage les Input sur la partie Outrun
/// </summary>

public class InputManagerOutRunPart : MonoBehaviour {

    public float turnFactor;
   
    public Vector3 beginTouch;
    public Vector3 endTouch;
    public Vector3 swapDirection;
    public GameObject[] paths;
    public Transform[] rainbowPaths;
    public GameObject rainbowEffect;
    public PlayerStatManager playerStat;
    public bool canSwap;
    public bool canUseInput;
    public bool isTuto;
    private bool checkTuto;
    public GameObject ReadyGo;
    public GameObject Tuto;
    public GameObject tutoInputLeft;
    public GameObject tutoInputRight;
    public Text boost;
    public OtherCarsAndScoreAssetManager CarsAndScoreManager;
    public ActivateAndDesactivatePopTravaux popTravaux;


    // Use this for initialization
    void Start () {

#if UNITY_WEBGL || UNITY_STANDALONE
        tutoInputLeft.SetActive(true);
        tutoInputRight.SetActive(true);
#endif

        canSwap = false;
        canUseInput = true;
        isTuto = true;
        beginTouch = Vector3.zero;
        endTouch = Vector3.zero;
        playerStat = GetComponent<PlayerStatManager>();
        GetComponent<Animator>().Play("PlayerPath3", 0, 0.0f);
        StartCoroutine(InputUpdate());
    }

    IEnumerator TakeScreenShot()
    {
        for (int i = 0; i < 25; i++)
        { 
                ScreenCapture.CaptureScreenshot("screenShot" + i, 1);
            yield return null;
        }
    }

    IEnumerator InputUpdate()
    {
        while (canUseInput)
        {

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                //StartCoroutine(TakeScreenShot());
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }

#if UNITY_WEBGL || UNITY_STANDALONE
        KeyboardInput();
#endif
#if UNITY_ANDROID || UNITY_IOS
            Tap();
            //Swap();
#endif
            TutoUpdate();
            yield return null;
        }
        yield break;
    }
    void TutoUpdate()
    {
        if(!boost.gameObject.activeSelf && !checkTuto && isTuto && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            checkTuto = true;
            if (popTravaux != null)
            {
                popTravaux.enabled = true;
            }
            StartCoroutine(KillTutoAnim());     
        }
    }

    IEnumerator KillIsTuto()
    {
        yield return new WaitForSeconds(3f);
        isTuto = false;
    }

    IEnumerator KillTutoAnim()
    {
        StartCoroutine(KillIsTuto());

        Tuto.GetComponent<Animator>().Play("endTuto", 0, 0.0f);
        yield return new WaitForSeconds(1.05f);
        Tuto.SetActive(false);
        ReadyGo.SetActive(true);
        CarsAndScoreManager.enabled = true;
        canSwap = true;
    }

    void KeyboardInput()
    {
        if (canSwap && !isTuto)
        {
            bool isPressed = false;
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && canSwap)
            {
                canSwap = false;
                isPressed = true;
                playerStat.CurrentRoad--;
            }
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow)) && canSwap)
            {
                canSwap = false;
                isPressed = true;
                playerStat.CurrentRoad++;
            }

            if (playerStat.CurrentRoad > 3)
            {
                playerStat.CurrentRoad = 3;
            }
            else if (playerStat.CurrentRoad < 0)
            {
                playerStat.CurrentRoad = 0;
            }

            if(isPressed)
            MoveCar(paths[playerStat.CurrentRoad].transform);
        }
    }

    void Tap()
    {
        if (canSwap && !isTuto)
        {
            if (Input.GetMouseButtonDown(0))
            {
                beginTouch = Input.mousePosition;
                CheckTapPosition();
            }
        }
    }

    void Swap()
    {
        if (canSwap)
        {
            if (Input.GetMouseButtonDown(0))
            {
                beginTouch = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                endTouch = Input.mousePosition;
                swapDirection = endTouch - beginTouch;
                beginTouch = Vector3.zero;
                endTouch = Vector3.zero;
                CheckSwapDirection();
            }
        }
    }

    void CheckTapPosition()
    {
        canSwap = false;
        if(beginTouch.x > (Screen.width *0.5f))
        {
            playerStat.CurrentRoad++;
        }
        else
        {
            playerStat.CurrentRoad--;
        }

        if (playerStat.CurrentRoad > 3)
        {
            playerStat.CurrentRoad = 3;
        }
        else if (playerStat.CurrentRoad < 0)
        {
            playerStat.CurrentRoad = 0;
        }

        MoveCar(paths[playerStat.CurrentRoad].transform);
    }

    void CheckSwapDirection()
    {
        canSwap = false;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        if (swapDirection.x > 0)
        {
            playerStat.CurrentRoad++;
        }
        else
        {
            playerStat.CurrentRoad--;
        }

        if (playerStat.CurrentRoad > 3)
        {
            playerStat.CurrentRoad = 3;
        }
        else if (playerStat.CurrentRoad < 0)
        {
            playerStat.CurrentRoad = 0;
        }

        MoveCar(paths[playerStat.CurrentRoad].transform);
    }

    public void MoveCar(Transform nextPath)
    {
        PlayerTriggerManager playerTriggerManager = GetComponent<PlayerTriggerManager>();

        // commenté à cause d'Apple
		SoundManager.instance.LoadSound("dérapage" + Random.Range(1, 3), 0.2f, Random.Range(0.95f, 1.05f));

        if (playerTriggerManager.isInBoost)
        {

            if (playerTriggerManager.boosts[0].activeSelf)
            {
                playerTriggerManager.boosts[0].SetActive(false);
                playerTriggerManager.boosts[0].GetComponent<Animator>().Play("boostLoop", 0, 0.0f);
            }
            if (playerTriggerManager.boosts[1].activeSelf)
            {
                playerTriggerManager.boosts[1].SetActive(false);
                playerTriggerManager.boosts[1].GetComponent<Animator>().Play("boostLoop", 0, 0.0f);
            }
            if (playerTriggerManager.boosts[2].activeSelf)
            {
                playerTriggerManager.boosts[2].SetActive(false);
                playerTriggerManager.boosts[2].GetComponent<Animator>().Play("boostLoop", 0, 0.0f);
            }
            if (playerTriggerManager.boosts[3].activeSelf)
            {
                playerTriggerManager.boosts[3].SetActive(false);
                playerTriggerManager.boosts[3].GetComponent<Animator>().Play("boostLoop", 0, 0.0f);
            }

            playerTriggerManager.boosts[GetComponent<PlayerStatManager>().CurrentRoad].SetActive(true);
        }

        StartCoroutine(ChangeTexture(0.05f));
        StartCoroutine(LerpPosition(nextPath));
    }

    IEnumerator ChangeTexture(float atTimer)
    {
        yield return new WaitForSeconds(atTimer);
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("PlayerPath" + (GetComponent<PlayerStatManager>().CurrentRoad+1), 0, 0.0f);
        yield return new WaitForSeconds(0.1f);
        rainbowEffect.transform.position = rainbowPaths[GetComponent<PlayerStatManager>().CurrentRoad].position;
        yield break;
    }

    IEnumerator ChangeBoostAnimIn(float timer)
    {
        PlayerTriggerManager playerTriggerManager = GetComponent<PlayerTriggerManager>();
        yield return new WaitForSeconds(timer);
        playerTriggerManager.boosts[GetComponent<PlayerStatManager>().CurrentRoad].GetComponent<Animator>().Play("boostLoop", 0, 0.0f);
    }

    IEnumerator LerpPosition(Transform TargetPath)
    {
        while ( Mathf.Abs(TargetPath.position.x - transform.position.x) > 0.001f)
        {
            transform.position = new Vector3( Mathf.Lerp(transform.position.x, TargetPath.position.x,Time.deltaTime*turnFactor), transform.position.y, transform.position.z);
            yield return transform;
        }
        canSwap = true;
        
        yield break;
    }

}
