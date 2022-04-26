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
        animator.SetTrigger(isAttackingHash);
		GameObject createdCurseball = Instantiate(curseball, shotPoint.position, shotPoint.rotation);
		createdCurseball.GetComponent<Rigidbody>().velocity = direction * attackSpeed;
    }

    void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
