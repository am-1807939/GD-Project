using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{

	public Transform target;

    public float lookRadius = 2f;
    public float amount = 10f;

    void Start()
    {
        if (target == null) {

			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}
    }


    void Update()
    {
        
        if (target == null)
			return;
        
        //get the distance between the chaser and the target
		float distance = Vector3.Distance(transform.position,target.position);


		if (distance <= lookRadius)
		{

			
			transform.LookAt(target);

				transform.position += transform.forward * 10f * Time.deltaTime;
        }
    }


    void OnTriggerEnter(Collider collision)
    {
        if (this.tag == "hp" && collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Health>().ApplyHeal(amount);
            Destroy (gameObject);
        }
        else if (this.tag == "mp" && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Mana>().RefillMana(amount);
            Destroy (gameObject);
        }
    }

        void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
