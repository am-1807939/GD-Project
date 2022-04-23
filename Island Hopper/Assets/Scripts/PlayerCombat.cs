using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Animator animator;
    public float turnTime = 0.1f;
    float turnVelocity;

    int isAttackingHash;
    int attackNumHash;

    public Transform mainCam;

    public Transform attackPoint;
    public float attackRange = 1f;
    public float attackDamage = 30f;
    public float attackCooldown = 1f;
    public float knockbackStrength = 1f;
    private float nextAttack = 0f;
    public AudioSource attackAudioSrc;


    public LayerMask enemyLayers;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isAttackingHash = Animator.StringToHash("isAttacking");
        attackNumHash = Animator.StringToHash("attackNum");
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time>=nextAttack){

            bool attackPressed = Input.GetKeyDown(KeyCode.Mouse0);

            if (attackPressed)
            {
                Attack();
                nextAttack=Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        animator.SetInteger(attackNumHash, (animator.GetInteger(attackNumHash) + 1) % 2);
        animator.SetTrigger(isAttackingHash);
        attackAudioSrc.Play();
        this.GetComponent<PlayerMovement>().CheckWalkAndRunSound();
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);


        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Collider[] hitEnemies = Physics.OverlapCapsule(attackPoint.position, attackPoint.position + new Vector3(0, 1f, 0) , attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies) {
            enemy.GetComponent<EnemyHealth>().ApplyDamage(attackDamage);
            enemy.GetComponent<EnemyController>().receiveKnockback(knockbackStrength, enemy.transform.position - transform.position);
        }

    }

    void OnDrawGizmos() {

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
