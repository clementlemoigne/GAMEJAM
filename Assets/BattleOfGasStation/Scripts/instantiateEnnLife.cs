using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instantiateEnnLife : MonoBehaviour {

    public UpdateEnnemyBattleOfSandwich ennemy;
    public int life;
    public Image EnnLife;
    public GameObject[] lifes;
	// Use this for initialization
	void Start () {

        life = ennemy.life;
        lifes = new GameObject[life];
        for (int i = 0; i < life; i++)
        {
            lifes[i] = Instantiate(EnnLife.gameObject,transform.position,Quaternion.identity);
            lifes[i].transform.SetParent(transform);
            lifes[i].transform.localPosition = Vector3.right * (i*150.0f);
            lifes[i].transform.localScale = Vector3.one;
        }

    }
}
