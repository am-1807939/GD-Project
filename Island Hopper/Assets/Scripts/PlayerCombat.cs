using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    Animator animator;

    public float turnTime = 0.1f;
    float turnVelocity;

    int isAttackingHash;

    public Transform mainCam;

    public Transform attackPoint;
    public float attackRange = 1f;
    public float attackDamage = 30f;

    public LayerMask enemyLayers;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {

        bool attackPressed = Input.GetKeyDown(KeyCode.Mouse0);

        if (attackPressed)
        {
            Attack();
        }


        if (Input.GetKeyDown(KeyCode.K))
            animator.SetTrigger("isHit");
    }

    void Attack()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        animator.SetTrigger(isAttackingHash);
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);


        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Collider[] hitEnemies = Physics.OverlapCapsule(attackPoint.position, attackPoint.position + new Vector3(0, 1f, 0) , attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies) {
            enemy.GetComponent<EnemyHealth>().ApplyDamage(attackDamage);
        }

    }

    void OnDrawGizmos() {

        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
