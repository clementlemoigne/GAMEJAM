using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateEnnemyBattleOfSandwich : MonoBehaviour
{

    public float speed;
    public float health;
    public float damage;
    public int life;
    public bool canUseMoveUpdate;
    public bool useMegaPattern;
    public bool isLostALife;
    public bool canLoseLife;
    public GameObject Projectile;
    public Transform Player;
    public Transform CornerLeft;
    public Transform CornerRight;
    public Transform Middle;
    public LerpScore ScoreNiv;
    public Animator EnnBar;
    public Animator ennAnimator;
    public Text Score;
    public PlayerStatManager playerStatManager;
    public InputManagerBattleOfSandwichPart inputManager;
    public PlayerTriggerBattleOfSandwich playerTriggerManager;
    public instantiateEnnLife EnnLife;
    public SpriteRenderer ennemyRenderer;
    public GameObject mobilecontrol;
    public GameObject PnJSpawner;
    public float moveTimerMin;
    public float moveTimerMax;
    public float speedRotationProjectile;
    public float steeringFactorProjectile;
    public float projectileLifeSec;
    public int MaxShotFirstPattern;
    public int MaxShotThirdPattern;
    public int MaxShotFourthPattern;

    public float timerBetweenShotFirstPattern;
    public float timerBetweenMegaPattern;
    public float timeBetweenShootThirdPattern;
    public float timeBetweenShootFourthPattern;
    // Use this for initialization
    void Start()
    {
        canLoseLife = true;
        isLostALife = false;
        useMegaPattern = false;
        canUseMoveUpdate = true;

        StartCoroutine(UpdateEnnemy());
    }

    IEnumerator UpdateEnnemy()
    {
        yield return new WaitForSeconds(3.0f);
        float moveTimer = 0.0f;
        int chanceTypeShoot = 0;

        while (canUseMoveUpdate)
        {

            
            if (health <= 0)
            {
                health = 100.0f;
                life--;
                isLostALife = true;
                StartCoroutine(DammageEffect());
                Destroy(EnnLife.lifes[life]);

                if(life == 0)
                {
                    EnnBar.transform.parent.gameObject.SetActive(false);
                    ScoreNiv.targetNumber = playerStatManager.score;
                    inputManager.MobileUnShoot();
                    inputManager.canShoot = false;
                    yield return StartCoroutine(MoveTo(Vector3.right * Middle.position.x,false,true));
                    yield break;
                }
                else
                {
                    SoundManager.instance.LoadSound("ColereBoss", 0.7f, 1.0f);
                    EnnBar.Play("EnnemyLife", 0, health / 100.0f);
                }
                
            }


            if (!isLostALife)
            {
                yield return StartCoroutine(MoveTo(Vector3.right * Random.Range(CornerLeft.position.x, CornerRight.position.x),false,false));

                ennAnimator.SetBool("Left", false);
                ennAnimator.SetBool("Right", false);

                chanceTypeShoot = Random.Range(0, 3);

                switch (chanceTypeShoot)
                {

                    case 0:
                        {
                            StartCoroutine(FirstPattern(Random.Range(1,4)));
                            break;
                        }
                    case 1:
                        {
                            StartCoroutine(ThirdPattern(0.1f, Random.Range(1, 4)));
                            break;
                        }
                    case 2:
                        {
                            StartCoroutine(FourthPattern(0.1f, Random.Range(1, 4)));
                            break;
                        }
                }

                moveTimer = Random.Range(moveTimerMin, moveTimerMax);
                yield return new WaitForSeconds(moveTimer);
            }
            else
            {

                if(!useMegaPattern)
                {
                    useMegaPattern = true;
                    ennAnimator.Play("Max_fury", 0, 0.0f);
                    yield return new WaitForSeconds(2.0f);
                    yield return StartCoroutine(MoveTo(Vector3.right * CornerLeft.position.x,false,false));
                    canLoseLife = true;
                    yield return StartCoroutine(MoveTo(Vector3.right * CornerRight.position.x,true,false));
                }
                isLostALife = false;
                useMegaPattern = false;
            }
            ennAnimator.Play("Max_wait", 0, 0.0f);
            yield return null;
        }
    }

    public IEnumerator DammageEffect()
    {
        GetComponent<BoxCollider>().enabled = false;
        canLoseLife = false;

        while (!canLoseLife)
        {
            yield return new WaitForSeconds(0.4f);
        }

        GetComponent<BoxCollider>().enabled = true;
        yield break;
    }

    IEnumerator MoveTo(Vector3 nextpath, bool useMegaPattern, bool useEndMove)
    {

        float delta = 0.05f;
        float timer = 0.0f;
        float positionY = transform.position.y;
        float positionZ = transform.position.z;
        int superPatternType = 0;

        if (useMegaPattern)
        {
            superPatternType = Random.Range(0, 3);
        }

        nextpath = nextpath + (Vector3.up * positionY) + (Vector3.forward * positionZ);

        Vector3 EnnToPosition = nextpath - transform.position;

        if(EnnToPosition.x > 0)
        {
            ennAnimator.SetBool("Right",true);
        }
        else
        {
            ennAnimator.SetBool("Left",true);
        }

        float distance = EnnToPosition.magnitude;
        bool shootInMove = true;

        while (EnnToPosition.magnitude > delta)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextpath, Time.deltaTime * speed);
            EnnToPosition = nextpath - transform.position;

            if (!useEndMove)
            {

                if (useMegaPattern)
                {
                    timer += Time.deltaTime;

                    if (timer >= timerBetweenMegaPattern)
                    {
                        timer = 0.0f;

                        switch (superPatternType)
                        {
                            case 0:
                                {
                                    StartCoroutine(FirstPattern(1));
                                    break;
                                }
                            case 1:
                                {
                                    StartCoroutine(SecondPattern(1));
                                    break;
                                }
                            case 2:
                                {
                                    StartCoroutine(ThirdPattern(0.0f, 3));
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    if ((EnnToPosition.magnitude / distance) < 0.5f && shootInMove)
                    {
                        shootInMove = false;
                        StartCoroutine(SecondPattern(0));
                    }
                }
            }
            
            yield return null;
        }

        if (useEndMove)
        {
            ennAnimator.Play("Max_Vomi", 0, 0.0f);
            
        }

        yield break;
    }

    //SimpleShot into the player
    IEnumerator FirstPattern(int nbShoot)
    {
        Vector3 EnnToPlayer = Player.position - transform.position;
        EnnToPlayer.Normalize();

        for (int i = 0; i < nbShoot; i++)
        {
            GameObject tempProjectile = Instantiate(Projectile, transform.position + Vector3.up * 0.05f, Quaternion.identity);
            
            ennAnimator.Play("Max_attack", 0, 0.0f);
            tempProjectile.GetComponent<ProjectileStat>().Direction = EnnToPlayer;
            Destroy(tempProjectile, projectileLifeSec);
            yield return new WaitForSeconds(timerBetweenShotFirstPattern);
        }

        yield break;
    }

    //simple shoot pattern
    IEnumerator SecondPattern(int nbShoot)
    {

        for (int i = 0; i < nbShoot; i++)
        {
            GameObject tempProjectile = Instantiate(Projectile, transform.position + Vector3.up * 0.05f, Quaternion.identity);
            ennAnimator.Play("Max_attack", 0, 0.0f);
            tempProjectile.GetComponent<ProjectileStat>().Direction = Vector3.back;
            Destroy(tempProjectile, projectileLifeSec);
            yield return null;
        }

        yield break;
    }

    IEnumerator ThirdPattern(float timer, int nbShoot)
    {

        Vector3 DirectionLeft = Vector3.left * 0.5f + Vector3.up * 0.0f + Vector3.back * 1.0f;
        Vector3 DirectionRight = Vector3.right * 0.5f + Vector3.up * 0.0f + Vector3.back * 1.0f;

        yield return new WaitForSeconds(timer);

        float deltaShoot = 1.0f / nbShoot;

        for (int i = 0; i < nbShoot; i++)
        {
            GameObject tempProjectile = Instantiate(Projectile, transform.position + Vector3.up * 0.05f, Quaternion.identity);
            ennAnimator.Play("Max_attack", 0, 0.0f);
            if (i == 0)
                tempProjectile.GetComponent<ProjectileStat>().Direction = Vector3.Lerp(DirectionLeft, DirectionRight, 0);
            else
                tempProjectile.GetComponent<ProjectileStat>().Direction = Vector3.Lerp(DirectionLeft, DirectionRight, deltaShoot * i);

            Destroy(tempProjectile, projectileLifeSec);
            yield return new WaitForSeconds(timeBetweenShootThirdPattern);
        }

        yield break;
    }

    IEnumerator FourthPattern(float timer, int nbShoot)
    {
        yield return new WaitForSeconds(timer);
        Vector3 EnnToPlayer = Player.position - transform.position;
        EnnToPlayer.Normalize();

        for (int i = 0; i < nbShoot; i++)
        {
            GameObject tempProjectile = Instantiate(Projectile, transform.position + Vector3.up * 0.05f, Quaternion.identity);
            ennAnimator.Play("Max_attack", 0, 0.0f);
            tempProjectile.GetComponent<ProjectileStat>().Direction = Vector3.back;
            tempProjectile.GetComponent<ProjectileStat>().useSteeringEffect = true;
            if (EnnToPlayer.x < 0)
            {
                tempProjectile.GetComponent<ProjectileStat>().SteeringAttenuation = -steeringFactorProjectile;
                tempProjectile.GetComponent<ProjectileStat>().rotationSpeed = -speedRotationProjectile;
            }
            else
            {
                tempProjectile.GetComponent<ProjectileStat>().SteeringAttenuation = steeringFactorProjectile;
                tempProjectile.GetComponent<ProjectileStat>().rotationSpeed = speedRotationProjectile;
            }
            Destroy(tempProjectile, projectileLifeSec);
            yield return new WaitForSeconds(timeBetweenShootFourthPattern);
        }
        yield break;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player" && canLoseLife)
        {
            playerStatManager.score += 300;
            Score.text = "Score : " + playerStatManager.score;
            SoundManager.instance.LoadSound("Toucher_bossNew", 2f, 1.0f);
            health -= playerStatManager.damage;
            EnnBar.Play("EnnemyLife", 0, health / 100.0f);
            EnnBar.speed = 0.0f;
        }
    }
}
