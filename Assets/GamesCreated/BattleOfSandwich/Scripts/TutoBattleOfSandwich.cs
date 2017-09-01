using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoBattleOfSandwich : MonoBehaviour {

    public UpdateEnnemyBattleOfSandwich updateEnnemy;
    public BoxCollider ennCollider;
    public InputManagerBattleOfSandwichPart inputManager;
    public GameObject ReadyGo;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTuto();
        }
    }

    public void EndTuto()
    {

        updateEnnemy.enabled = true;
        ennCollider.enabled = true;
        inputManager.enabled = true;
        SoundManager.instance.LoadMusic("F1 Havas Battle sandwitch", 1.0f);
        ReadyGo.SetActive(true);
        gameObject.SetActive(false);
    }

}
