using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    
    public float lookRadius = 10f;
	private float attackSpeed = 20.0f;

	public Transform shotPoint;

	public Transform target;

    Animator animator;
	public GameObject curseball;
    int isAttackingHash;
    int attackNumHash;

    public float attackDamage = 30f;
    public float attackCooldown = 1f;

    private float nextAttack = 0f;

    public GameObject fleeEffect;
    public GameObject summonedEnemy;
    public int numberOfSummons = 3;
    private Vector3 prevPosition;


	// Use this for initialization
	void Start () 
	{
		// if no target specified, assume the player
		if (target == null) {

			if (GameObject.FindWithTag ("Player")!=null)
			{
				target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
			}
		}

        animator = GetComponent<Animator>();
        isAttackingHash = Animator.StringToHash("isAttacking");
        attackNumHash = Animator.StringToHash("attackNum");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target == null)
			return;
        
        //get the distance between the chaser and the target
		float distance = Vector3.Distance(transform.position,target.position);


		if (distance <= lookRadius)
		{

			// face the target
			transform.LookAt(target);

			
				if(Time.time>=nextAttack){

					// Invoke("Attack", attackDelay);
					// Attack();
					nextAttack=Time.time + attackCooldown;
					animator.SetTrigger(isAttackingHash);
        		}

		} 
	}


	    void Attack()
    {
		Vector3 direction = (target.transform.position - transform.position).normalized;
		GameObject createdCurseball = Instantiate(curseball, shotPoint.position, shotPoint.rotation);
		createdCurseball.GetComponent<Rigidbody>().velocity = direction * attackSpeed;
    }

	void Flee() {
		prevPosition = transform.position;
		Invoke("summonEnemy", 0.5f);
		Instantiate(fleeEffect, transform.position + new Vector3 (0f, 2f, 0f), transform.rotation);
		transform.position = new Vector3(Random.Range(-30f,30f), 0f, Random.Range(-30f,30f));
	}

	void summonEnemy() {
		for (int i = 0; i < numberOfSummons; i++) {
            Instantiate(summonedEnemy, prevPosition + new Vector3 (Random.Range(-1f,1f), 2f, Random.Range(-1f,1f)), transform.rotation);
         }
	}

    void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
