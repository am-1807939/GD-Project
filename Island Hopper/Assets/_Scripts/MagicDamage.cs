using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamage : MonoBehaviour {
	
	public float damageAmount = 10.0f;
	public bool destroySelfOnImpact = false;	// variables dealing with exploding on impact (area of effect)
	public float delayBeforeDestroy = 0.0f;
	public GameObject explosionPrefab;


	void OnTriggerEnter(Collider collision)						// used for things like bullets, which are triggers.  
	{
		
			if (collision.gameObject.GetComponent<EnemyHealth> () != null) {	// if the hit object has the Health script on it, deal damage
				collision.gameObject.GetComponent<EnemyHealth> ().ApplyDamage (damageAmount);
		
				if (destroySelfOnImpact) {
					Destroy (gameObject, delayBeforeDestroy);	  // destroy the object whenever it hits something
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}

			if (collision.gameObject.GetComponent<Health> () != null) {	// if the hit object has the Health script on it, deal damage
				collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
		
				if (destroySelfOnImpact) {
					Destroy (gameObject, delayBeforeDestroy);	  // destroy the object whenever it hits something
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
	}


	void OnCollisionEnter(Collision collision) 						// this is used for things that explode on impact and are NOT triggers
	{	

			if (collision.gameObject.GetComponent<EnemyHealth> () != null) {	// if the hit object has the Health script on it, deal damage
				collision.gameObject.GetComponent<EnemyHealth> ().ApplyDamage (damageAmount);
			
				if (destroySelfOnImpact) {
					Destroy (gameObject, delayBeforeDestroy);	  // destroy the object whenever it hits something
				}
			
				if (explosionPrefab != null) {
					Instantiate (explosionPrefab, transform.position, transform.rotation);
				}
			}
	}
	
}