using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePnJ : MonoBehaviour
{
    public bool isSpecialPnj;
    public Text Score;
    public PlayerStatManager playerStat;
    public Animator pnjAnimator;
    public GameObject ennProjectile;
    public GameObject malus;
    public float speed;
    public bool IsDamage;
    public string SoundName;
    public string MoveName;
    public string DamageName;
    // Use this for initialization
    void Start()
    {
        IsDamage = false;

        if (GameObject.Find("Player") != null)
            playerStat = GameObject.Find("Player").GetComponent<PlayerStatManager>();

        if(GameObject.Find("Score") != null)
        Score = GameObject.Find("Score").GetComponent<Text>();

        StartCoroutine(UpdatePosition());
    }

    IEnumerator UpdatePosition()
    {
        while (true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x >= 1.0f)
            {
                Destroy(gameObject);
            }

            yield return null;
        }
    }

    void OnCollisionEnter(Collision other)
    {


        if(!IsDamage)
        StartCoroutine(StopPnj());

        if (other.transform.tag == "Player")
        {
            if (isSpecialPnj)
            {
                GameObject tempProjectile = Instantiate(ennProjectile, transform.position + Vector3.up * 0.05f, Quaternion.identity);
                tempProjectile.name = "projectile PnJ";
                Vector3 PnjToPlayer = playerStat.transform.position - transform.position;
                PnjToPlayer.Normalize();
                tempProjectile.GetComponent<ProjectileStat>().Direction = PnjToPlayer;
            }

            GameObject tempMalus = Instantiate(malus, transform.position, Quaternion.identity);
            tempMalus.GetComponent<Animator>().enabled = true;
            tempMalus.GetComponent<Animator>().Play("malus", 0, 0.0f);
            tempMalus.transform.SetParent(transform, false);
            playerStat.score -= 100;
            if (playerStat.score <= 0)
            {
                playerStat.score = 0;
            }
            Score.text = "Score : " + playerStat.score;
        }
    }

    IEnumerator StopPnj()
    {
        IsDamage = true;
        Debug.Log("Stop");
        SoundManager.instance.LoadSound(SoundName, 1.5f, 1.0f);
        pnjAnimator.Play(DamageName, 0, 0.0f);

       

        float lastSpeed = speed;
        speed = 0.0f;
        yield return new WaitForSeconds(1.0f);
        pnjAnimator.Play(MoveName, 0, 0.0f);
        speed = lastSpeed;
        IsDamage = false;
        yield break;
    }

}
