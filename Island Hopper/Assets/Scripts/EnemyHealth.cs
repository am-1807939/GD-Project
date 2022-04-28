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
    public GameObject healthDrop;
    public GameObject ManaDrop;
    public float HPDropChance = 0.3f;
    public float MPDropChance = 0.3f;

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

        if (GetComponent<EnemyController>() != null)
            GetComponent<EnemyController>().enabled = false; 

        if (GetComponent<BossController>() != null)
            GetComponent<BossController>().enabled = false; 

        // destroy after delay and when animation is over
        Invoke("dropItems", animator.GetCurrentAnimatorStateInfo(0).length + deathDelay);
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length + deathDelay);
    }

    void dropItems()
    {
        if (itemDrop != null) {
            Instantiate(itemDrop, transform.position + new Vector3 (0f, 2f, 0f), transform.rotation);
        }

        if(Random.Range(0f, 1f) <= HPDropChance && healthDrop != null)
        {
            Instantiate(healthDrop, transform.position + new Vector3 (Random.Range(0f, 1f), 0f, Random.Range(0f, 1f)), transform.rotation);
        }

        if(Random.Range(0f, 1f) <= MPDropChance && ManaDrop != null)
        {
            Instantiate(ManaDrop, transform.position + new Vector3 (Random.Range(0f, 1f), 0f, Random.Range(0f, 1f)), transform.rotation);
        }
    }
	
}
