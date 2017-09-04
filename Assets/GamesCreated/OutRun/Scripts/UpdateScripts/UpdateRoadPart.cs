using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRoadPart : MonoBehaviour {

    public float speed;
    public float idRoad;
    public float deltaRoadPositionZ;
    private Renderer roadPartRenderer;
    private Vector2 SpeedVector;
    public BezierCurve bezierCurve;
    public DelegateHandler delegateHandler;
    public UpdateEnnemyCarOnTheRoad updateEnnemyCar;
    public PlayerTriggerManager playerTriggerManager;
    public AudioSource audioSource;
    // Use this for initialization
    void Start () {

        roadPartRenderer = GetComponent<Renderer>();
        SpeedVector = new Vector2(0.0f, 0.0625f);
        DelegateHandler.ChangeGlobalspeed += ChangeSpeed;
        DelegateHandler.ChangeGlobaltexture += ChangeTexture;
        delegateHandler = GameObject.FindGameObjectWithTag("event").GetComponent<DelegateHandler>();
        StartCoroutine(UpdateRoadPartSpeed());
    }

    IEnumerator UpdateRoadPartSpeed()
    {
        while (true)
        {
            roadPartRenderer.material.mainTextureOffset += SpeedVector * speed * Time.deltaTime;
            transform.position = bezierCurve.GetPointAt(deltaRoadPositionZ * idRoad);
            yield return transform;
        }
    }

    public IEnumerator TargetSpeedAt(float targetspeed, float speedAttenuation)
    {
        updateEnnemyCar.stopCurrentAction = true;
        updateEnnemyCar.deceleratefactor = updateEnnemyCar.decelerationWhenNotInvinsibleFactor;
        

        while (speed < targetspeed-5.0f)
        {
            speed = Mathf.Lerp(speed, targetspeed, Time.deltaTime* speedAttenuation);
            delegateHandler.OnDamage(speed);
            audioSource.pitch = speed / targetspeed;
            yield return speed;
        }

        yield return null;
    }

    public IEnumerator TargetSpeedAtBis(float targetspeed,float speedAttenuation)
    {
        speed = targetspeed;

        while (speed >= 75.0f)
        {
            speed = Mathf.Lerp(speed, 70.0f, Time.deltaTime * speedAttenuation);
            delegateHandler.OnDamage(speed);
            audioSource.pitch = speed / 75.0f;

            yield return speed;
        }

        Debug.Log("EndBoost");

        updateEnnemyCar.stopCurrentAction = true;
        updateEnnemyCar.deceleratefactor = updateEnnemyCar.decelerationWhenNotInvinsibleFactor;

        playerTriggerManager.isInBoost = false;
        playerTriggerManager.boosts[0].SetActive(false);
        playerTriggerManager.boosts[1].SetActive(false);
        playerTriggerManager.boosts[2].SetActive(false);
        playerTriggerManager.boosts[3].SetActive(false);

        yield return null;
    }

    void ChangeSpeed(float value)
    {
        speed = value;
    }

    void ChangeTexture(Texture2D texture)
    {
        roadPartRenderer.material.mainTexture = texture;
    }

    void OnDisable()
    {
        DelegateHandler.ChangeGlobalspeed -= ChangeSpeed;
        DelegateHandler.ChangeGlobaltexture -= ChangeTexture;
    }

    void OnDestroy()
    {
        DelegateHandler.ChangeGlobalspeed -= ChangeSpeed;
        DelegateHandler.ChangeGlobaltexture -= ChangeTexture;
    }

}
