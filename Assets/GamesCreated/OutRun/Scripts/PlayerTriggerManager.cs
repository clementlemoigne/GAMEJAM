using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ce script permet les intéractions avec les différents objets, active les boosts et les collisions.
/// </summary>
public class PlayerTriggerManager : MonoBehaviour {

    public PlayerStatManager playerStat;
    
    public Transform car;

    public GameObject[] boosts;
    public Sprite[] shapes;
    public Animator playerBar;
    public Animator cameraAnimator;
    public GameObject explosion;
    public GameObject speedUI;
    public GameObject noteParticle;
    public DelegateHandler delegateHandler;
    public UpdateRoadPart updateRoadPart;

    private Renderer playerRenderer;
    private Rigidbody playerRigidbody;
    private InputManagerOutRunPart inputManager;

    public Text sante;
    public Text combo;
    public Text score;
    public SpriteContainer noteContainer;
    public Animator turbo;
    public UpdateEnnemyCarOnTheRoad updateEnnemyCar;
    public bool useBoost;
    public bool isInBoost;
    public bool canBoost;
    public bool canColide;
    public bool canDamage;
    public bool isInvinsible;
    public int EnnemyDamage;

    public int pointMiss;
    public int pointNote;

    public float InvincibilityTimer;
    public float EndOfInvincibility;

    void Start()
    {
        canDamage = true;
        isInBoost = false;
        isInvinsible = false;
        canBoost = false;
        canColide = true;
        useBoost = true;
        playerRenderer = GetComponentInChildren<Renderer>();
        playerRigidbody = GetComponent<Rigidbody>();
        inputManager = playerStat.GetComponent<InputManagerOutRunPart>();
        StartCoroutine(UpdateMaxSpeedState());
    }

    IEnumerator UpdateMaxSpeedState()
    {
        while (true)
        {
            playerRigidbody.AddForce(Vector3.up * 0.00001f);
            // Si on a récupéré 6 notes, on lance le bonus arc-en-ciel
            if (playerStat.multiplier == 6 && canColide == true)
            {
                SoundManager.instance.LoadMusic("Invisibilite", 1.0f);
                speedUI.SetActive(true);
                isInvinsible = true;
                updateEnnemyCar.activeDecelerate = true;
                updateEnnemyCar.activeAccelerate = false;
                updateEnnemyCar.stopCurrentAction = true;
                updateEnnemyCar.deceleratefactor = updateEnnemyCar.decelerationWhenInvinsibleFactor;
                canColide = false;
                
                playerStat.multiplier = 1;
                combo.text = "Combo : " + 0;
                StartCoroutine(updateRoadPart.TargetSpeedAtBis(120.0f, 0.3f));
                turbo.Play("InvicibilityEffect", 0, 0.0f);

                if (cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("speedUpEffectIDLE"))
                {
                    cameraAnimator.Play("speedUpEffectIDLE", 0, 0.0f);
                }
                else
                {
                    cameraAnimator.Play("speedUpEffect", 0, 0.0f);
                }

                GetComponent<InputManagerOutRunPart>().rainbowEffect.SetActive(true);
                StartCoroutine(StopInvincibityIn(InvincibilityTimer));
                StartCoroutine(StopSpeedAnimationIn(InvincibilityTimer));
            }    

            if(canBoost)
            {
                boosts[0].SetActive(false);
                boosts[1].SetActive(false);
                boosts[2].SetActive(false);
                boosts[3].SetActive(false);
                boosts[playerStat.CurrentRoad].SetActive(true);
            }

            yield return playerStat;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "ennemy" && canDamage && inputManager.canSwap && playerStat.CurrentRoad == updateEnnemyCar.idCurrentPath)
        {
            //canColide = false;
            Debug.Log("Ennemy");
            updateEnnemyCar.canswap = false;
            speedUI.SetActive(false);
            StartCoroutine(DamageTheEnnemy(30.0f));
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if ((other.transform.tag == "backcar" || other.transform.tag == "asset") && canColide)
        {
            SoundManager.instance.LoadSound("chock1", 0.7f, 1.0f);

            if(other.transform.tag == "backcar")
            SoundManager.instance.LoadSound("Klaxson", 0.5f, 1.0f);

            canColide = false;
            useBoost = false;
            other.transform.GetComponentInChildren<BoxCollider>().enabled = false;
            delegateHandler.OnDamage(30.0f);
            updateEnnemyCar.activeDecelerate = false;
            updateEnnemyCar.activeAccelerate = true;

            playerStat.multiplier--;
            combo.text = "Combo : " + playerStat.multiplier;
          
            if (playerStat.multiplier < 1)
                playerStat.multiplier = 1;

            noteContainer.ChangeSprite(playerStat.multiplier - 1);
            playerStat.Health -= EnnemyDamage;    

            playerBar.Play("PlayerLife", 0, playerStat.Health / 100.0f);
            playerBar.speed = 0.0f;
            GameObject tempExplosion = Instantiate(explosion, car.transform.position, Quaternion.identity);
            StartCoroutine(updateRoadPart.TargetSpeedAt(70.0f, 1.0f));
            turbo.Play("IDLE", 0, 0.0f);
            cameraAnimator.Play("speedDownEffect", 0, 0.0f);
            Destroy(tempExplosion, 3.0f);
            StartCoroutine(ActivateUseBoostIn(0.7f));
            StartCoroutine(StopInvincibityIn(2f));
            turbo.Play("Damage", 0, 0.0f);
        }

        if (other.transform.tag == "1_2")
        {
            car.GetComponent<SpriteRenderer>().sprite = shapes[0];
        }

        if (other.transform.tag == "2_3")
        {
            car.GetComponent<SpriteRenderer>().sprite = shapes[1];
        }

        if (other.transform.tag == "3_4")
        {
            car.GetComponent<SpriteRenderer>().sprite = shapes[2];
        }

        if (other.transform.tag == "score")
        {

            switch (playerStat.multiplier)
            {
                case 1:
                    {
                        SoundManager.instance.LoadSound("Bonus1", 0.7f, 1.0f);
                        break;
                    }
                case 2:
                    {
                        SoundManager.instance.LoadSound("Bonus2", 0.7f, 1.0f);
                        break;
                    }
                case 3:
                    {
                        SoundManager.instance.LoadSound("Bonus3", 0.7f, 1.0f);
                        break;
                    }
                case 4:
                    {
                        SoundManager.instance.LoadSound("Bonus4", 0.7f, 1.0f);
                        break;
                    }
                case 5:
                    {
                        SoundManager.instance.LoadSound("Bonus5", 0.7f, 1.0f);
                        break;
                    }
            }

            GameObject tempParticle = Instantiate(noteParticle, other.transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
            Destroy(tempParticle, 3.0f);
            other.transform.GetComponent<Renderer>().enabled = false;
            playerStat.score += pointNote * playerStat.multiplier;

            score.text = "Score : " + playerStat.score;
            combo.text = "Combo : " + (playerStat.multiplier);


            if (!isInvinsible)
            {
                noteContainer.ChangeSprite(playerStat.multiplier);
                playerStat.multiplier++;
            }

            Destroy(other.gameObject);
        }

        if (other.transform.tag == "cd" && canColide)
        {
            SoundManager.instance.LoadSound("Obstacle_gravier", 0.7f, 1.0f);
            delegateHandler.OnDamage(30.0f);
            turbo.Play("IDLE", 0, 0.0f);
            updateEnnemyCar.activeDecelerate = false;
            updateEnnemyCar.activeAccelerate = true;
            StartCoroutine(updateRoadPart.TargetSpeedAt(70.0f, 1.0f));
            Destroy(other.gameObject, 2.0f);
        }
    }
    IEnumerator ActivateUseBoostIn(float timer)
    {
        yield return new WaitForSeconds(timer);
        useBoost = true;
        turbo.Play("IDLE", 0, 0.0f);
        yield break;
    }

    public IEnumerator StopBoostIn(float timer)
    {
        StartCoroutine(StopSpeedUIIn(1.0f));
        yield return new WaitForSeconds(timer);
        Debug.Log("StopBoostIn");    
        canBoost = false;
        useBoost = true;
        yield break;
    }

    public IEnumerator StopInvincibityIn(float timer)
    {
        yield return new WaitForSeconds(timer);
        StartCoroutine(CanCollideIn(0.0f));
        isInvinsible = false;
        yield break;
    }

    public IEnumerator CanCollideIn(float timer)
    {
        yield return new WaitForSeconds(timer);
        canColide = true;
        yield break;
    }

    public IEnumerator DamageTheEnnemy(float speed)
    {
        InputManagerOutRunPart playerInputManager = GetComponent<InputManagerOutRunPart>();
        canDamage = false;
        bool canDamageEnn = false;
        //playerInputManager.canSwap = false;
        while (!canDamageEnn)
        {
            if (playerInputManager.canSwap)
            {
                canDamageEnn = true;
            }
            yield return null;
        }
       
        playerInputManager.canSwap = false;
        Vector3 playerBegin = car.position;
        Vector3 PlayerToEnnemy = Vector3.zero;   

        PlayerToEnnemy = updateEnnemyCar.transform.position - car.position;

        while (PlayerToEnnemy.magnitude > 0.1f)
        {
            car.position = Vector3.MoveTowards(car.position, updateEnnemyCar.transform.position, Time.deltaTime * speed);
            PlayerToEnnemy = updateEnnemyCar.transform.position - car.position;
            yield return car;
        }

        updateEnnemyCar.GoToHorizon(transform);
        GameObject tempExplosion = Instantiate(explosion, car.transform.position, Quaternion.identity);
        Destroy(tempExplosion, 3.0f);

        PlayerToEnnemy = playerBegin - car.position;

        while (PlayerToEnnemy.magnitude > 0.01f)
        {
            car.position = Vector3.MoveTowards(car.position, playerBegin, Time.deltaTime * speed);
            PlayerToEnnemy = playerBegin - car.position;
            yield return car;
        }

        car.position = playerBegin;

        playerInputManager.canSwap = true;
        updateEnnemyCar.canswap = true;
        //updateEnnemyCar.GetComponent<BoxCollider>().enabled = false;
        
        StartCoroutine(EnableColliderIn(1.0f));
        yield break;
    }

    public IEnumerator EnableColliderIn(float timer)
    {
        yield return new WaitForSeconds(timer);

        canDamage = true;

        yield break;
    }

    public IEnumerator StopSpeedUIIn(float timer)
    {
        yield return new WaitForSeconds(timer);
        speedUI.SetActive(false);
        yield break;
    }

    public IEnumerator StopSpeedAnimationIn(float timer)
    {
        yield return new WaitForSeconds(EndOfInvincibility);

        turbo.Play("EndInvicibility", 0, 0.0f);
        
        yield return new WaitForSeconds(timer - EndOfInvincibility);

        if (playerStat.checkHealth)
        {
            switch (playerStat.NvOutRun)
            {
                case 1:
                    {
                        SoundManager.instance.LoadMusic("F1 Havas Run1", 1.0f);
                        break;
                    }
                case 2:
                    {
                        SoundManager.instance.LoadMusic("F1 Havas Run 2", 1.0f);
                        break;
                    }
            }
        }
        GetComponent<InputManagerOutRunPart>().rainbowEffect.SetActive(false);
        noteContainer.ChangeSprite(0);
        turbo.Play("IDLE", 0, 0.0f);
        yield break;
    }

    public IEnumerator DamageAnim(int nbSwapAlphaColor , float SwapAlphaColorDelta)
    {
        Color alphazero = new Color(1, 1, 1, 0);

        for(int i = 0; i < nbSwapAlphaColor * 2 ; i++)
        {
            yield return new WaitForSeconds(SwapAlphaColorDelta);
            
            if( i%2 == 1)
            {
                playerRenderer.material.color = Color.white;
            }
            else
            {
                playerRenderer.material.color = alphazero;
            }

        }

        yield break;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "asset" && canColide)
        {
            SoundManager.instance.LoadSound("chock1", 0.7f, 1.0f);

            canColide = false;
            useBoost = false;
            other.transform.GetComponentInChildren<BoxCollider>().enabled = false;
            delegateHandler.OnDamage(30.0f);
            updateEnnemyCar.activeDecelerate = false;
            updateEnnemyCar.activeAccelerate = true;

            playerStat.multiplier--;
            combo.text = "Combo : " + playerStat.multiplier;

            if (playerStat.multiplier < 1)
                playerStat.multiplier = 1;

            noteContainer.ChangeSprite(playerStat.multiplier - 1);
            playerStat.Health -= EnnemyDamage;

            playerBar.Play("PlayerLife", 0, playerStat.Health / 100.0f);
            playerBar.speed = 0.0f;
            GameObject tempExplosion = Instantiate(explosion, car.transform.position, Quaternion.identity);
            StartCoroutine(updateRoadPart.TargetSpeedAt(70.0f, 1.0f));
            turbo.Play("IDLE", 0, 0.0f);
            cameraAnimator.Play("speedDownEffect", 0, 0.0f);
            Destroy(tempExplosion, 3.0f);
            StartCoroutine(ActivateUseBoostIn(0.7f));
            StartCoroutine(StopInvincibityIn(2f));
            turbo.Play("Damage", 0, 0.0f);
        }

    }
    

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "missTrigger" && useBoost && canColide)
        {
            speedUI.SetActive(true);
            SoundManager.instance.LoadSound("Boost",0.3f,1.0f);

            float chanceKlaxon = Random.Range(0.0f, 100.0f);

            if(chanceKlaxon > 50.0f)
            {
                SoundManager.instance.LoadSound("Klaxson", 0.5f,1.0f);
            }

            isInBoost = true;
            useBoost = false;

            if (cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("speedUpEffectIDLE"))
            {
                cameraAnimator.Play("speedUpEffectIDLE", 0, 0.0f);
            }
            else
            {
                cameraAnimator.Play("speedUpEffect", 0, 0.0f);
            }
            canBoost = true;
            playerStat.score += pointMiss * playerStat.multiplier;
            score.text = "Score : " + playerStat.score;

            updateEnnemyCar.deceleratefactor = updateEnnemyCar.decelerationWhenInvinsibleFactor;

            if (boosts[0].activeSelf)
            {
                boosts[0].SetActive(false);
                boosts[0].GetComponent<Animator>().Play("boostLoop", 0, 0.0f);
            }
            if (boosts[1].activeSelf)
            {
                boosts[1].SetActive(false);
                boosts[1].GetComponent<Animator>().Play("boostLoop", 0, 0.0f);
            }
            if (boosts[2].activeSelf)
            {
                boosts[2].SetActive(false);
                boosts[2].GetComponent<Animator>().Play("boostLoop", 0, 0.0f);
            }
            if (boosts[3].activeSelf)
            {
                boosts[3].SetActive(false);
                boosts[3].GetComponent<Animator>().Play("boostLoop", 0, 0.0f);
            }
            boosts[playerStat.CurrentRoad].SetActive(true);
            boosts[playerStat.CurrentRoad].GetComponent<Animator>().Play("IDLE", 0, 0.0f);
            StartCoroutine(StopBoostIn(0.2f));
            StartCoroutine(updateRoadPart.TargetSpeedAtBis(110.0f,1.0f));
        }
    }
}
