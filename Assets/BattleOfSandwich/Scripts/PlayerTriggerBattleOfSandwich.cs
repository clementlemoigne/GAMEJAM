using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerBattleOfSandwich : MonoBehaviour {

    public Animator PlayerBar;
    public UpdateEnnemyBattleOfSandwich updateEnnemy;
    public InputManagerBattleOfSandwichPart inputManager;
    private PlayerStatManager playerStatManager;
    public bool canBeDamage;

    void Start()
    {
        canBeDamage = true;
        playerStatManager = GetComponent<PlayerStatManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "ennemy" && canBeDamage)
        {
            canBeDamage = false;
            SoundManager.instance.LoadSound("Outch_nous", 0.7f, 1.0f);
            StartCoroutine(inputManager.DammageEffect());
            playerStatManager.Health -= updateEnnemy.damage;
            PlayerBar.Play("PlayerLife", 0, playerStatManager.Health / 100.0f);
            PlayerBar.speed = 0.0f;
        }

    }

}
