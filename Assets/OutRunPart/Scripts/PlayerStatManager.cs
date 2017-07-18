using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatManager : MonoBehaviour
{

    public float Health;
    public int score;
    public int multiplier = 1;
    public int CurrentRoad = 2;
    public int NvOutRun;
    public int damage;
    public int globalLevel;
    public int lostBonusScore;
    public bool checkHealth;
    public GameObject PopupGameOver;
    public LerpScore ScoreNiv;
    public string bonusScoreName;
    void Start()
    {
        Application.targetFrameRate = 30;
        checkHealth = true;

        if(Health > 0)
        StartCoroutine(CheckHealth());
    }

    IEnumerator CheckHealth()
    {
        while (checkHealth)
        {
            if (Health <= 1)
            {
                EndOfLevel();
                PopupGameOver.SetActive(true);
                PlayerPrefs.SetInt(bonusScoreName,PlayerPrefs.GetInt(bonusScoreName) - lostBonusScore);

                if(PlayerPrefs.GetInt(bonusScoreName) <= 0)
                {
                    PlayerPrefs.SetInt(bonusScoreName, 0);
                }

                SoundManager.instance.LoadSound("gameOver Sound", 1.0f, 1.0f);

            }
            yield return Health;
        }

    }

    public void EndOfLevel()
    {
        switch (globalLevel)
        {
            case (1):
                {
                    EndOfOutRun();
                    break;
                }
            case (2):
                {
                    EndOfBattleOfSandwich();
                    break;
                }
            case (3):
                {
                    EndOfBattleOfGasStation(false);
                    break;
                }
        }
    }

    public void EndOfOutRun()
    {
        PlayerTriggerManager playerTriggerManager = GetComponent<PlayerTriggerManager>();
        InputManagerOutRunPart inputManager = GetComponent<InputManagerOutRunPart>();
        ScoreNiv.targetNumber = score;
        playerTriggerManager.canColide = false;
        playerTriggerManager.canDamage = false;
        playerTriggerManager.GetComponent<AudioSource>().enabled = false;
        playerTriggerManager.GetComponent<Rigidbody>().isKinematic = true;
        playerTriggerManager.GetComponent<BoxCollider>().enabled = false;
        inputManager.enabled = false;
        checkHealth = false;
        GameObject.FindGameObjectWithTag("carManager").GetComponent<OtherCarsAndScoreAssetManager>().canInstantiate = false;
        GameObject.FindGameObjectWithTag("UI").SetActive(false);
        
    }

    public void EndOfBattleOfSandwich()
    {

        checkHealth = false;
        gameObject.SetActive(false);

        GameObject.Find("UI").SetActive(false);
        GameObject.FindGameObjectWithTag("pnjManager").SetActive(false);

    }

    public void EndOfBattleOfGasStation(bool useAnim)
    {
        if(ScoreNiv != null)
        ScoreNiv.targetNumber = score;
        
        GameObject.FindGameObjectWithTag("UI").SetActive(false);
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        GameObject.FindGameObjectWithTag("ennemy").SetActive(false);

        if(useAnim)
        GameObject.FindGameObjectWithTag("Finish").GetComponent<Animator>().enabled = true;

    }
}
