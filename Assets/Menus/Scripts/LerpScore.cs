using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LerpScore : MonoBehaviour {

    public bool useBonusScore;
    public bool isGlobalScore;
    public bool useSound;
    public LerpScore nextLerpScore;
    public PlayerStatManager playerStat;
    public float targetNumber;
    public float currentScore;
    public float scoreAmountDelta;
    public Text text;
    public bool launchUpdate;
	// Use this for initialization
	void Start () {
        //currentScore = 0;
        //text = GetComponent<Text>();
        
        if (useBonusScore)
        {
            targetNumber = PlayerPrefs.GetInt(playerStat.bonusScoreName);
        }

        StartCoroutine(CheckUpdateLerp());
    }
	
    public IEnumerator CheckUpdateLerp()
    {
        while (!launchUpdate)
        {
            Debug.Log("CheckUpdateLerp -> " + name + launchUpdate);
            yield return null;
        }

        Debug.Log("check -> " + name);

        StartCoroutine(UpdateLerp());
        yield break;
    }

	public IEnumerator UpdateLerp()
    {

        //yield return new WaitForSeconds(0.1f);
        if (!isGlobalScore)
        {
            while (currentScore > 0)
            {
                currentScore = (int)Mathf.MoveTowards(currentScore, 0, scoreAmountDelta);
                text.text = "" + currentScore;
                yield return text;
            }
            currentScore = 0;
        }
        else
        {
            while ((int)currentScore < (int)targetNumber)
            {
                currentScore = (int)Mathf.MoveTowards(currentScore, targetNumber, scoreAmountDelta);
                text.text = "" + currentScore;

                SoundManager.instance.LoadSound("Pickup_", 0.5f, 1.0f);

                yield return text;
            }
            currentScore = targetNumber;
        }
        if (nextLerpScore != null)
        {
            nextLerpScore.enabled = true;
            nextLerpScore.launchUpdate = true;
        }
        Debug.Log(name);
        text.text = "" + (int)currentScore;
        yield break;
    }


}
