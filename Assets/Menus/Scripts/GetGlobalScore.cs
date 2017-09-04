using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGlobalScore : MonoBehaviour {

    private LerpScore lerpScore;
    public float nbSec;
    public LerpScore scoreNiv;
    public LerpScore scoreBonusTemps;
    public LerpScore scoreFin;
    public bool firstLevel;

    // Use this for initialization
    IEnumerator Start () {


        scoreBonusTemps.targetNumber = nbSec - Time.timeSinceLevelLoad;
        scoreBonusTemps.targetNumber *= 10.0f;



        if(scoreBonusTemps.targetNumber <0)
        {
            scoreBonusTemps.targetNumber = 0;
        }

        scoreNiv.currentScore = scoreNiv.targetNumber;
        scoreFin.currentScore = scoreFin.targetNumber;
        scoreBonusTemps.currentScore = scoreBonusTemps.targetNumber;

        scoreNiv.text.text = "" + (int)scoreNiv.currentScore;
        scoreFin.text.text = "" + (int)scoreFin.currentScore;
        scoreBonusTemps.text.text = "" + (int)scoreBonusTemps.currentScore;

        scoreNiv.launchUpdate = true;

        lerpScore = GetComponent<LerpScore>();

        if(firstLevel)
            lerpScore.currentScore = 0;
        else
            lerpScore.currentScore = PlayerPrefs.GetInt("Score");

        lerpScore.targetNumber = scoreNiv.targetNumber + scoreFin.targetNumber + scoreBonusTemps.targetNumber + lerpScore.currentScore;

        yield return new WaitForSeconds(0.1f);
        lerpScore.enabled = true;
        PlayerPrefs.SetInt("Score", (int)lerpScore.targetNumber);

    }
}
