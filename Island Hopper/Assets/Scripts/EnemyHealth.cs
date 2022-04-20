using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	
	float healthPoints;
	public float maxHP = 100f;		
	public GameObject explosionPrefab;	

	void Start () 
	{
		healthPoints = maxHP;
	}
	
	
	public void ApplyDamage(float amount)
	{	
		healthPoints = healthPoints - amount;
        if (healthPoints <= 0) {
            Die();
        }	
	}

    void Die() {

        if (explosionPrefab!=null) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		}

        Destroy(gameObject);
    }
	
}
