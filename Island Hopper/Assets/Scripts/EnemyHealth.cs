using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	
	float healthPoints;
    Animator animator;

    int isDeadHash;
    int isHitHash;

	public float maxHP = 100f;		

    public float deathDelay = 1f;
	public GameObject explosionPrefab;
    public Slider healthBar;

	void Start () 
	{
		healthPoints = maxHP;
        animator = GetComponent<Animator>();
        isHitHash = Animator.StringToHash("isHit");
        isDeadHash = Animator.StringToHash("isDead");
	}

    private void Update()
    {
        healthBar.value = healthPoints / maxHP;
    }


    public void ApplyDamage(float amount)
	{	

		healthPoints = healthPoints - amount;
        
        if (healthPoints <= 0) {
            Die();
        } else {
            animator.SetTrigger(isHitHash);
        }
	}

    void Die() {

        if (explosionPrefab!=null) {
				Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		}

        animator.SetTrigger(isDeadHash);

        // Disable all collider so attacking during death animation is not possible
        foreach(Collider c in GetComponents<Collider> ()) {
         c.enabled = false; 
        }

        // destroy after delay and when animation is over
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + deathDelay);
    }
	
}
