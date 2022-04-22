using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCombat : MonoBehaviour
{
    Animator animator;

    public GameObject fireball;
    public Transform shotPoint;
    private float attackSpeed = 20.0f;

    int isAttackingHash;

    public float turnTime = 0.1f;
    float turnVelocity;
    public Transform mainCam;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            animator.SetTrigger(isAttackingHash);
            this.GetComponent<PlayerMovement>().CheckWalkAndRunSound();
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            GameObject createdFireball = Instantiate(fireball, shotPoint.position, shotPoint.rotation);
            createdFireball.GetComponent<Rigidbody>().velocity = shotPoint.up * attackSpeed;
        }
    }
}
