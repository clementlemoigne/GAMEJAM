using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateGazTankFill : MonoBehaviour {

    public bool IsPlayer;
    public bool canUpdateGasTank;
    public float speedGazAmount;
    public float BPM;
    public CurrentTapUpdate currentTapUpdate;
    public Text score;
    public int scoreOptimalFill;
    public int scoreNormalFill;
    private Image gazTank;
    private PlayerStatManager playerStatManager;
    
    // Use this for initialization
    void Start ()
    {
        gazTank = GetComponent<Image>();
        StartCoroutine(UpdateGazTank());

        if(IsPlayer)
        {
            playerStatManager = GameObject.Find("Player").GetComponent<PlayerStatManager>(); 
        }
    }

    IEnumerator UpdateGazTank()
    {
        yield return new WaitForSeconds(3.0f);
        canUpdateGasTank = true;
        while (canUpdateGasTank)
        {
            if (IsPlayer)
            {
                if (currentTapUpdate.isInOptimalFill)
                    playerStatManager.score += scoreOptimalFill;
                else
                    playerStatManager.score += scoreNormalFill;

                score.text = "Score : " + playerStatManager.score;
            }
            gazTank.fillAmount += speedGazAmount;   
            yield return new WaitForSeconds(60.0f/BPM);
        }
    }

}
