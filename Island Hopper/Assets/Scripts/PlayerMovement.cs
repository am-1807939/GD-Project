using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;

    public float movementSpeed = 4.0f;
    public CharacterController controller;
    public float turnTime = 0.1f;
    float turnVelocity;
    public Transform mainCam;

    private float gravity = -9.81f;
    private Vector3 velocity;
    public AudioSource walkAudioSrc;
    public AudioSource runAudioSrc;
    public AudioSource jumpAuidoSrc;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
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

        if (isPlaying(animator, "Attack04") == false)
        {
            if (direction.magnitude >= 0.1)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
                controller.Move(movementSpeed * Time.deltaTime * moveDir.normalized);

            }
        }


        if (controller.isGrounded && velocity.y < 0 && (isPlaying(animator, "JumpStart") == false || isPlaying(animator, "JumpEnd") == false))
        {
            velocity.y = -2;
        }

        if (jumpPressed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(1 * -2.0f * gravity);
            jumpAuidoSrc.Play();
            if (walkAudioSrc.isPlaying)
            {
                walkAudioSrc.Pause();
                walkAudioSrc.PlayDelayed(1f);
            }
            if (runAudioSrc.isPlaying)
            {
                runAudioSrc.Pause();
                runAudioSrc.PlayDelayed(1f);
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
            walkAudioSrc.Play();
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
            walkAudioSrc.Stop();
        }


            if (!isRunning && (forwardPressed && runPressed))
            {
                animator.SetBool(isRunningHash, true);
                walkAudioSrc.Stop();
                runAudioSrc.Play();
                movementSpeed = 8.0f;
            }


            if (isRunning && (!forwardPressed || !runPressed))
            {
                walkAudioSrc.Play();
                runAudioSrc.Stop();
            animator.SetBool(isRunningHash, false);
                movementSpeed = 4.0f;
            }

            if (jumpPressed)
                animator.SetTrigger(isJumpingHash);

        bool isPlaying(Animator anim, string stateName)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                    anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                return true;
            else
                return false;
        }
    }
}
