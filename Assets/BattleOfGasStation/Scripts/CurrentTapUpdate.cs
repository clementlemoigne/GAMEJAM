

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentTapUpdate : MonoBehaviour
{

    public bool isPlayer;
    public bool canUseTapInput;
    public bool isInOptimalFill;
    public float deltaAmount;
    public float fillDownamount;
    public float distanceToOptimalFill;
    public float optimalAmountGaz;
    public float currentAmountGaz;
    public RectTransform optimalTarget;
    public RectTransform upCurrentTap;
    public float currentDistanceToOptimalTarget;
    public float checkCursorHeight;
    public PlayerStatManager playerStatManager;
    public UpdateGazTankFill gazTankFill;
    public Animator optimalTargetAnimator;
    public Animator PlayerAnimation;
    public LerpScore ScoreNiv;
    public GameObject PopupScore;
    public GameObject PopupGameOver;
    public Image currentTap;
    //ennemy part

    public float timerBetweenTapMin;
    public float timerBetweenTapMax;
    public bool canTap;
    // Use this for initialization
    void Start()
    {
        currentTap = GetComponent<Image>();
        currentDistanceToOptimalTarget = 0.0f;
        checkCursorHeight = 0.0f;
        if (isPlayer)
        {
#if UNITY_WEBGL || UNITY_STANDALONE

            deltaAmount = 10;
            fillDownamount = 0.12f;
#endif
#if UNITY_ANDROID || UNITY_IOS
            deltaAmount = 10;
            fillDownamount = 0.08f;
#endif
            StartCoroutine(PlayerUpdateCurrentTap());
        }
        else
        {
            fillDownamount = 0.2f;
            canTap = true;
            StartCoroutine(EnnemyUpdateCurrentTap());
        }
    }

    IEnumerator EnnemyUpdateCurrentTap()
    {

        yield return new WaitForSeconds(3.0f);
        optimalTargetAnimator.enabled = true;

        canUseTapInput = true;

        while (canUseTapInput)
        {

            if (gazTankFill.GetComponent<Image>().fillAmount > 0.99f)
            {
                canUseTapInput = false;
                PopupGameOver.SetActive(true);
                SoundManager.instance.LoadSound("gameOver Sound", 1.0f, 1.0f);
               
                playerStatManager.EndOfBattleOfGasStation(false);
                
                Debug.Log("Game Over");
            }

            float currentDistanceCurrentTap = Mathf.Lerp(130.0f, 370.0f, currentTap.fillAmount);

            checkCursorHeight = optimalTarget.anchoredPosition.y - currentDistanceCurrentTap;

            currentDistanceToOptimalTarget = Mathf.Abs(checkCursorHeight);

            if(currentDistanceToOptimalTarget < distanceToOptimalFill)
            {
                canTap = false;
                gazTankFill.speedGazAmount = optimalAmountGaz;
                isInOptimalFill = true;
            }
            else
            {

                if(checkCursorHeight > 0)
                {
                    canTap = true;
                }
                else
                {
                    canTap = false;
                }
                
                gazTankFill.speedGazAmount = currentAmountGaz;
                isInOptimalFill = false;
            }

            if (PlayerAnimation != null)
            {

                if (currentTap.fillAmount > 0.0f && currentTap.fillAmount <= 0.33f)
                {
                    PlayerAnimation.SetInteger("state", 0);
                }
                if (currentTap.fillAmount > 0.33f && currentTap.fillAmount <= 0.66f)
                {
                    PlayerAnimation.SetInteger("state", 1);
                }
                if (currentTap.fillAmount > 0.66f && currentTap.fillAmount <= 1.0f)
                {
                    PlayerAnimation.SetInteger("state", 2);
                }
            }

            if (canTap)
            {
                yield return new WaitForSeconds(Random.Range(timerBetweenTapMin, timerBetweenTapMax));
                currentTap.fillAmount = Mathf.Lerp(currentTap.fillAmount, 1.0f, Time.deltaTime * deltaAmount);
            }

            

            if (currentTap.fillAmount > 0.0f)
                currentTap.fillAmount -= Time.deltaTime * fillDownamount;

            if (currentTap.fillAmount <= 0.0f)
                currentTap.fillAmount = 0.0f;

            yield return null;
    
        }
    }

    IEnumerator PlayerUpdateCurrentTap()
    {
        yield return new WaitForSeconds(3.0f);
        optimalTargetAnimator.enabled = true;

        canUseTapInput = true;

        while (canUseTapInput)
        {

            if (gazTankFill.GetComponent<Image>().fillAmount > 0.99f)
            {
                canUseTapInput = false;
                SoundManager.instance.LoadMusic("F1 Havas WESTERN BATTLE", 1.0f);
                playerStatManager.EndOfBattleOfGasStation(true);
            }

            float currentDistanceCurrentTap = Mathf.Lerp(130.0f, 370.0f, currentTap.fillAmount);

            checkCursorHeight = optimalTarget.anchoredPosition.y - currentDistanceCurrentTap;

            currentDistanceToOptimalTarget = Mathf.Abs(checkCursorHeight);

            if (currentDistanceToOptimalTarget < distanceToOptimalFill)
            {
                gazTankFill.speedGazAmount = optimalAmountGaz;
                isInOptimalFill = true;
            }
            else
            {
                gazTankFill.speedGazAmount = currentAmountGaz;
                isInOptimalFill = false;
            }


            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                currentTap.fillAmount = Mathf.Lerp(currentTap.fillAmount, 1.0f, Time.deltaTime * deltaAmount);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (PlayerAnimation != null)
            {

                if (currentTap.fillAmount > 0.0f && currentTap.fillAmount <= 0.33f)
                {
                    PlayerAnimation.SetInteger("state", 0);
                }
                if (currentTap.fillAmount > 0.33f && currentTap.fillAmount <= 0.66f)
                {
                    PlayerAnimation.SetInteger("state", 1);
                }
                if (currentTap.fillAmount > 0.66f && currentTap.fillAmount <= 1.0f)
                {
                    PlayerAnimation.SetInteger("state", 2);
                }
            }

            if (currentTap.fillAmount > 0.0f)
                currentTap.fillAmount -= Time.deltaTime * Mathf.Lerp(fillDownamount, 2.0f, Time.deltaTime * deltaAmount);

            if (currentTap.fillAmount <= 0.0f)
                currentTap.fillAmount = 0.0f;

            yield return null;

        }    

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "optimalTarget")
        {
            Debug.Log("Hit!!!");
        }
    }
}
