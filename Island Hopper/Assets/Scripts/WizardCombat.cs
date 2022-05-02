using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WizardCombat : MonoBehaviour
{
    Animator animator;

    public GameObject fireball;
    public Transform shotPoint;
    public float SingleManaUsage = 10f;
    public float AreaManaUsage = 50f;
    private float attackSpeed = 20.0f;

    private Mana playerMana;

    private int mode = 0;
    int isAttackingHash;
    int isWalkingHash;
    int isRunningHash;

    private float nextAttack = 0f;
    public float attackCooldown = 1f;
    public float turnTime = 0.1f;
    float turnVelocity;
    public Transform mainCam;
    public CinemachineVirtualCamera secondCam;

    public GameObject laserAttack;
    public GameObject magicAura;
    public AudioSource attackAudioSrc;
    public AudioSource laserAudioSrc;
    public GameObject hoverEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMana = GameObject.Find("Player").GetComponent<Mana>();
        isAttackingHash = Animator.StringToHash("isAttacking");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingHash = Animator.StringToHash("isWalking");
        hoverEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            switchMode();
        }

        if(Time.time>=nextAttack){


        if (mode == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerMana.ConsumeMana(SingleManaUsage))
            {
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

                animator.SetTrigger(isAttackingHash);
                attackAudioSrc.Play();
                this.GetComponent<PlayerMovement>().CheckWalkAndRunSound();
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);

                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                GameObject createdFireball = Instantiate(fireball, shotPoint.position, shotPoint.rotation);
                createdFireball.GetComponent<Rigidbody>().velocity = shotPoint.up * attackSpeed;

                nextAttack=Time.time + attackCooldown;
            }
        }
        else if (mode == 1)
        {

            Vector3 point = mainCam.GetComponent<SelectArea>().hit.point;

            hoverEffect.transform.position = point;

            if (Input.GetKeyDown(KeyCode.Mouse0) && playerMana.ConsumeMana(AreaManaUsage))
            {
		        GameObject createdLaser = Instantiate(laserAttack, point, transform.rotation);
                    laserAudioSrc.Play();

                switchMode();

                nextAttack=Time.time + attackCooldown;
            }

        }

        }


    }


    void switchMode() {
        mode = (mode + 1) % 2;
            secondCam.Priority = mode * 10;
            if (mode == 0)
            {
                GetComponent<PlayerMovement>().enabled = true;
                transform.parent.gameObject.GetComponent<CharacterSwitch>().enabled = true;
                magicAura.SetActive(false);
                Cursor.visible = false;
                mainCam.GetComponent<SelectArea>().mode3 = false;
                hoverEffect.SetActive(false);
            }
            else if (mode == 1)
            {
                GetComponent<PlayerMovement>().enabled = false;
                transform.parent.gameObject.GetComponent<CharacterSwitch>().enabled = false;
                animator.SetBool(isWalkingHash, false);
                animator.SetBool(isRunningHash, false);
                magicAura.SetActive(true);
                Cursor.visible = true;
                mainCam.GetComponent<SelectArea>().mode3 = true;
                hoverEffect.SetActive(true);
            }
    }
}
