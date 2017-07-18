using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoBattleOfGasStation : MonoBehaviour {

    public CurrentTapUpdate player;
    public CurrentTapUpdate ennemy;
    public GameObject ReadyGo;
    public GameObject tutoInputSpace;
    public UpdateGazTankFill playerJauge;
    public UpdateGazTankFill ennemyJauge;
    public AudioSource audioBG;
    public Animator optimalPlayerTarget;
    public Animator optimalEnnemyTarget;

    void Start()
    {
#if UNITY_WEBGL || UNITY_STANDALONE
        tutoInputSpace.SetActive(true);
#endif
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EndTuto();
        }
    }

    public void EndTuto()
    {
        player.enabled = true;
        ennemy.enabled = true;
        ReadyGo.SetActive(true);
        playerJauge.enabled = true;
        ennemyJauge.enabled = true;
        SoundManager.instance.LoadMusic("F1 Havas BAttleHouseDark_2", 1.0f);
        optimalPlayerTarget.enabled = true;
        optimalEnnemyTarget.enabled = true;
        gameObject.SetActive(false);
    }

}
