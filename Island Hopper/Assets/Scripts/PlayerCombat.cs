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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        bool attackPressed = Input.GetKeyDown(KeyCode.Mouse0);

        if (attackPressed)
        {
            animator.SetTrigger(isAttackingHash);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }


        if (Input.GetKeyDown(KeyCode.K))
            animator.SetTrigger("isHit");  
    }


}
