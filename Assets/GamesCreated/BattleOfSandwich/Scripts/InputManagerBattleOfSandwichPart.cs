using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// Script permettant de gérer tous les déplacements et les actions du personnage
/// En fonction des Inputs reçus (UI ou Touchs)
/// </summary>
public class InputManagerBattleOfSandwichPart : MonoBehaviour
{

    public bool canUseInput;
    public bool canShoot;
    public bool canUnShoot;
    public bool canShootWithStamina;
    public bool canMove;
    public bool readyGo;
    public Vector3 Direction;
    public float deltashoot;
    public float speedMove;
    public float speedRotationProjectile;
    public float speedStaminaRecovery;
    public float StaminaCostPerShoot;
    public float directionFactorProjectile;
    public float steeringfactor;
    public float stamina;
    public Image staminaBar;
    public GameObject projectile;
    public SpriteRenderer playerRenderer;
    public Animator K_Controller;
    public Transform Middle;
    public UpdateEnnemyBattleOfSandwich updateEnnemy;
    public GameObject mobileControls;
    public GameObject EndAnim;
    public GameObject FP;
    public GameObject FP2;
    public GameObject BG;
    public GameObject UI;
    private BoxCollider collision;
    private Rigidbody playerRigidbody;
    
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 30;
#if UNITY_STANDALONE
        GameObject.FindGameObjectWithTag("mobile").SetActive(false);
        speedMove = 1000;
#endif

#if UNITY_WEBGL
        GameObject.FindGameObjectWithTag("mobile").SetActive(false);
        speedMove = 500;
#endif

#if UNITY_ANDROID || UNITY_IOS
        speedMove = 500;
#endif
        canUnShoot = false;
        readyGo = false;
        canUseInput = true;
        canShoot = true;
        canMove = false;
        stamina = 100.0f;
        playerRigidbody = GetComponent<Rigidbody>();
        collision = GetComponent<BoxCollider>();
        StartCoroutine(UpdatePosition());
        K_Controller.Play("K_wait", 0, 0.0f);
    }



    IEnumerator UpdatePosition()
    {
        yield return new WaitForSeconds(3.0f);
        canMove = true;
        canShoot = true;
        readyGo = true;
        while (canUseInput)
        {

            if(canShoot && Input.touchCount == 0)
            {
                MobileUnShoot();
            }

            if(updateEnnemy.life == 0)
            {
                canUseInput = false;
                mobileControls.SetActive(false);
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

#if UNITY_WEBGL || UNITY_STANDALONE
            KeyboardInput();
#endif
#if UNITY_ANDROID || UNITY_IOS

            UpdateMobileInput();
#endif
            playerRigidbody.velocity = Vector3.zero;

            yield return null;
        }

        canMove = false;
        playerRigidbody.velocity = Vector3.zero;

        yield return MoveTo(Vector3.right*Middle.position.x);

        gameObject.SetActive(false);
        updateEnnemy.gameObject.SetActive(false);

        UI.SetActive(false);
        BG.SetActive(false);
        FP.SetActive(false);
        FP2.SetActive(false);


        yield break;
    }

    void UpdateMobileInput()
    {
        if (canMove)
        {
            playerRigidbody.AddForce(Direction);
        }
    }

    public void KeyboardInput()
    {

        

        if (Input.GetKeyUp(KeyCode.Space) && canUnShoot)
        {
            if (readyGo)
                StartCoroutine(UnShoot(deltashoot));
        }


        if (Input.GetKey(KeyCode.Space) && canShoot && canMove)
        {
            if (readyGo)
                StartCoroutine(Shoot(deltashoot));
           
        }
        

        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)))
        {
            TranslateLeft();
        }
        else if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            TranslateRight();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && canMove)
        {
            StartCoroutine(Avoid());
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow) && !canMove)
        {
            StartCoroutine(UnAvoid());
        }
        if ((Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)))
        {
            K_Controller.SetBool("Right", false);
            K_Controller.SetBool("Left", false);
            K_Controller.Play("K_wait", 0, 0.0f);
            UnTranslate();
        }

    }

    public IEnumerator DammageEffect()
    {
        Color whiteNoAlpha = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        for(int i = 0; i < 3; i++)
        {
            playerRenderer.color = whiteNoAlpha;
            yield return new WaitForSeconds(0.2f);
            playerRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
        GetComponent<PlayerTriggerBattleOfSandwich>().canBeDamage = true;
         yield break;
    }

    public void UnTranslate()
    {        
            Direction = Vector3.zero;
            playerRigidbody.velocity = Vector3.zero;
    }

    public void TranslateLeft()
    {
        if (canMove)
        {
            K_Controller.SetBool("Left", true);
            Direction = Vector3.left * speedMove * Time.deltaTime;
            playerRigidbody.AddForce(Direction);
        }
    }

    public void TranslateRight()
    {
        if (canMove)
        {
            K_Controller.SetBool("Right", true);
            Direction = Vector3.right * speedMove * Time.deltaTime;
            playerRigidbody.AddForce(Direction);
        }
    }

    IEnumerator Avoid()
    {
        canMove = false;
        yield return new WaitForEndOfFrame();
        SoundManager.instance.LoadSound("Deplacement_sous", 0.7f, 1.0f);
        K_Controller.Play("K_crouch", 0, 0.0f);
        collision.center = Vector3.zero + Vector3.forward * 0.15f;
        UnShoot(0.5f);
        yield break;
    }

    IEnumerator UnAvoid()
    {
        K_Controller.Play("K_up", 0, 0.0f);
        playerRigidbody.velocity = Vector3.zero;
        collision.center = Vector3.zero - Vector3.forward * 0.05f;
        canShoot = true;
        canMove = true;
        yield return new WaitForSeconds(0.1f);
        K_Controller.Play("K_wait", 0, 0.0f);
    }

    IEnumerator UnShoot(float timer)
    {
        canShoot = false;
        yield return new WaitForSeconds(timer);
        Debug.Log("UnShoot");
        canShoot = true;
    }

    IEnumerator Shoot(float timer)
    {

        if (canUnShoot)
        {
            yield break;
        }


        canUnShoot = true;
        Debug.Log("Shoot");
        SoundManager.instance.LoadSound("Lancer_sandwitch" + Random.Range(1,5), 0.7f, Random.Range(0.95f, 1.05f));
        K_Controller.Play("K_attack", 0, 0.0f);
        GameObject tempProjectile = Instantiate(projectile, transform.position + Vector3.up * 0.05f, Quaternion.identity);

        tempProjectile.GetComponent<ProjectileStat>().Direction = Vector3.forward;

        if (Direction.x < 0)
        {
            tempProjectile.GetComponent<ProjectileStat>().useSteeringEffect = true;
            tempProjectile.GetComponent<ProjectileStat>().SteeringAttenuation = -steeringfactor;
        }
        if (Direction.x > 0)
        {
            tempProjectile.GetComponent<ProjectileStat>().useSteeringEffect = true;
            tempProjectile.GetComponent<ProjectileStat>().SteeringAttenuation = steeringfactor;
        }
        Destroy(tempProjectile, 2.5f);

        yield return new WaitForSeconds(timer);
        canUnShoot = false;

        if (canShoot)
        StartCoroutine(Shoot(timer));

        yield break;

    }

    IEnumerator MoveTo(Vector3 nextpath)
    {

        float delta = 0.05f;
        float positionY = transform.position.y;
        float positionZ = transform.position.z;
        nextpath = nextpath + (Vector3.up * positionY) + (Vector3.forward * positionZ);

        Vector3 PlayerToPosition = nextpath - transform.position;

        if (PlayerToPosition.x > 0)
        {
            K_Controller.SetBool("Right", true);
        }
        else
        {
            K_Controller.SetBool("Left", true);
        }

        float distance = PlayerToPosition.magnitude;

        while (PlayerToPosition.magnitude > delta)
        {

            transform.position = Vector3.MoveTowards(transform.position, nextpath, Time.deltaTime * 0.3f);
            PlayerToPosition = nextpath - transform.position;

            yield return null;
        }

        K_Controller.SetBool("Left", false);
        K_Controller.SetBool("Right", false);
        K_Controller.Play("K_wait", 0,0.0f);
        
        yield return new WaitForSeconds(4.0f);
        //gameObject.SetActive(false);


        yield break;
    }

    IEnumerator ChargeStaminaBar()
    {

        while (stamina < 100.0f)
        {
            stamina += speedStaminaRecovery * Time.deltaTime;
            staminaBar.rectTransform.localScale = Vector3.right * (stamina / 100.0f) + Vector3.up + Vector3.forward;
            yield return stamina;
        }

        canShootWithStamina = true;

        yield break;
    }

    public void MobileShoot()
    {
        if(canShoot && readyGo)
            StartCoroutine(Shoot(deltashoot));
    }

    public void MobileUnShoot()
    {
        if(canUnShoot && readyGo)
            StartCoroutine(UnShoot(deltashoot));
    }

    public void MobileAvoid()
    {
        if (canMove)
            StartCoroutine(Avoid());
    }

    public void MobileUnAvoid()
    {
        if (!canMove)
            StartCoroutine(UnAvoid());
    }

    public void MobileUnTranslate()
    {
        if (canMove)
        {
            K_Controller.SetBool("Right", false);
            K_Controller.SetBool("Left", false);
            K_Controller.Play("K_wait", 0, 0.0f);
            UnTranslate();
        }
    }
}