using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public float speed = 20.0f;
    public float lookRadius = 10f;

	public float minDist = 1f;
	public Transform target;

    Animator animator;
    int isWalkingHash;
    int isAttackingHash;


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
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");
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

		//so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
		if(distance > minDist)	{
            animator.SetBool(isWalkingHash, true);
            animator.SetBool(isAttackingHash, false);
            transform.position += transform.forward * speed * Time.deltaTime;	
        } else {
            animator.SetBool(isAttackingHash, true);
        }

        }
	}

	// Set the target of the chaser
	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

    void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

}
