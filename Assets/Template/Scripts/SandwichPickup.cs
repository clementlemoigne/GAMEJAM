using UnityEngine;

public class SandwichPickup : MonoBehaviour 
{
    public GameObject eatingParticles;

    private float bigScaleSize;

    private void Start()
    {
        bigScaleSize = ScoreManager.instance.GetComponent<SandwichSpawner>().getBigScaleSize();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("barbedorSandwich"))
        {
            int points = 1;

            //si c'est un gros sandwich il rapporte plus de points
            if (collision.transform.localScale.x == bigScaleSize)
            {
                points = 5;
            }

            Destroy(collision.gameObject);

            //augmente le score
            ScoreManager.instance.addPoint(points);

            //particles de sandwich
            GameObject instance =  Instantiate(eatingParticles,transform.position, eatingParticles.transform.rotation);
            Destroy(instance, 1);
        }
    }
}
