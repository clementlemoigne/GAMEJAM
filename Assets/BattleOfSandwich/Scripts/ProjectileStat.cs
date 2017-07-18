using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStat : MonoBehaviour {

    public GameObject explosion;
    public GameObject shadow;
    public Vector3 Direction;
    public bool canUpdatePosition;
    public bool useSteeringEffect;
    public bool useRotationEffect;
    public float speed;
    public float rotationSpeed;
    private float steeringFactor;
    public float SteeringAttenuation;
    private Rigidbody projectileRigidbody;
    private BoxCollider colliderProjectile;
	// Use this for initialization
	void Start () {
        canUpdatePosition = true;
        projectileRigidbody = GetComponent<Rigidbody>();
        colliderProjectile = GetComponentInChildren<BoxCollider>();
        StartCoroutine(UpdateProjectile());

	}
	
    IEnumerator UpdateProjectile()
    {
        steeringFactor = 0.0f;

        while (canUpdatePosition)
        {
            projectileRigidbody.velocity = Vector3.zero;
            projectileRigidbody.AddForce(Direction * speed);
            projectileRigidbody.AddForce(Vector3.right*((Direction.x*0.01f) + steeringFactor));

            if (useSteeringEffect)
            {
                steeringFactor += SteeringAttenuation;
            }

            if(useRotationEffect)
            {
                transform.Rotate(Vector3.up*rotationSpeed);
            }

            yield return null;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        shadow.SetActive(false);
        colliderProjectile.GetComponent<Animator>().speed = 0.0f;
        colliderProjectile.GetComponentInChildren<Renderer>().enabled = false;
        GameObject tempAsset = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(tempAsset,0.2f);
		SoundManager.instance.LoadSound("Sandwitch_loupé", 0.5f, Random.Range(0.95f,1.05f));
        canUpdatePosition = false;
        colliderProjectile.enabled = false;
    }

}
