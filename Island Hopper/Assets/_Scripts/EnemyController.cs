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
    Rigidbody rb;
    int isWalkingHash;
    int isAttackingHash;

    public Transform attackPoint;
    public float attackRange = 1f;
    public float attackDamage = 30f;
    public float attackCooldown = 1f;
    // public float attackDelay = 1f;
    private float nextAttack = 0f;

    public LayerMask playerLayer;
    private AudioSource attackSrc;



    // Use this for initialization
    void Start()
    {
        // if no target specified, assume the player
        if (target == null)
        {

            if (GameObject.FindWithTag("Player") != null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (GetComponent<AudioSource>() != null)
        {
            attackSrc = GetComponent<AudioSource>();
			attackSrc.Stop();
        }
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        //get the distance between the chaser and the target
        float distance = Vector3.Distance(transform.position, target.position);


        if (distance <= lookRadius)
        {

            // face the target
            transform.LookAt(target);

            //so long as the chaser is farther away than the minimum distance, move towards it at rate speed.
            if (distance > minDist)
            {
                animator.SetBool(isWalkingHash, true);
                animator.SetBool(isAttackingHash, false);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else
            {
                if (Time.time >= nextAttack)
                {

                    bool attackPressed = Input.GetKeyDown(KeyCode.Mouse0);

                    // Invoke("Attack", attackDelay);
                    // Attack();
                    nextAttack = Time.time + attackCooldown;
                }
                animator.SetBool(isAttackingHash, true);
            }

        }
        else
        {
            animator.SetBool(isWalkingHash, false);
        }
    }


    void Attack()
    {

        Collider[] players = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in players)
        {
            player.GetComponent<Health>().ApplyDamage(attackDamage);

            if (attackSrc != null)
            {
                attackSrc.Play();
            }

        }

    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void receiveKnockback(float strength, Vector3 direction)
    {
        rb.AddForce(direction * strength);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
