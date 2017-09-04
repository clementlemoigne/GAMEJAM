using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePopupScore : MonoBehaviour {

    public LerpScore scoreNiv;
    public LerpScore bonusFin;
    public LerpScore bonusTemps;
    public LerpScore scoretotal;

    // Use this for initialization
    void Start () {
        StartCoroutine(UpdatePopup());
	}

    IEnumerator UpdatePopup()
    {
        yield return new WaitForSeconds(1.0f);
        bool canUpdate = true;

        
        while (canUpdate)
        {
            if (Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.Space))
            {
                scoreNiv.enabled = true;
                bonusFin.enabled = true;
                bonusTemps.enabled = true;
                scoretotal.enabled = true;
                scoreNiv.currentScore = 0;
                bonusFin.currentScore = 0;
                bonusTemps.currentScore = 0;
                scoretotal.currentScore = scoretotal.targetNumber;
                canUpdate = false;
            }
                
            yield return null;
        }
        yield break;
    }
}
