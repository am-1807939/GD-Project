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
    public GameObject itemDrop;

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

        GetComponent<EnemyController>().enabled = false; 

        // destroy after delay and when animation is over
        Invoke("dropItems", animator.GetCurrentAnimatorStateInfo(0).length + deathDelay);
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + deathDelay);
    }

    void dropItems()
    {
        if (itemDrop != null) {
            Instantiate(itemDrop, transform.position, transform.rotation);
        }
    }
	
}
