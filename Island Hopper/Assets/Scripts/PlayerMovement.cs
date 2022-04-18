using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isAttackingHash;

    public float movementSpeed = 2.0f;
    public CharacterController controller;
    public float turnTime = 0.1f;
    float turnVelocity;
    public Transform mainCam;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);
        bool attackPressed = Input.GetKeyDown(KeyCode.Mouse0);

        if (isPlaying(animator,"Attack04") == false)
        {
            if (direction.magnitude >= 0.1)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                controller.Move(movementSpeed * Time.deltaTime * moveDir.normalized);
            }

            if (jumpPressed)
                controller.Move(new Vector3(0, 1, 0));

        }


   



        if (!isWalking && forwardPressed)
            animator.SetBool(isWalkingHash, true);
        if (isWalking && !forwardPressed)
            animator.SetBool(isWalkingHash, false);

        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);
            movementSpeed = 8.0f;
        }


        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
            movementSpeed = 2.0f;
        }

        if (jumpPressed)
            animator.SetTrigger(isJumpingHash);

        if (Input.GetKeyDown(KeyCode.K))
            animator.SetTrigger("isHit");

        if (attackPressed)
        {
            animator.SetTrigger(isAttackingHash);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
}
