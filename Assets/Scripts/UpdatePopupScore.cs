using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePopupScore : MonoBehaviour {

    public LerpScore scoreLvl;
    public LerpScore bonusEnd;
    public LerpScore bonusTime;
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
                scoreLvl.enabled = true;
                bonusEnd.enabled = true;
                bonusTime.enabled = true;
                scoretotal.enabled = true;
                scoreLvl.currentScore = 0;
                bonusEnd.currentScore = 0;
                bonusTime.currentScore = 0;
                scoretotal.currentScore = scoretotal.targetNumber;
                canUpdate = false;
            }
                
            yield return null;
        }
        yield break;
    }
}
