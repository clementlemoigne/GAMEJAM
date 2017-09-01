using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupScoreEvent : MonoBehaviour {

    public GameObject popupRecapScore;

    public void PopupScore()
    {
        popupRecapScore.SetActive(true);
        SoundManager.instance.LoadMusic("F1 Havas Victory Sun", 1.0f);
    }

}
