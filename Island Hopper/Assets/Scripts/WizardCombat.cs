using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WizardCombat : MonoBehaviour
{
    Animator animator;

    public GameObject fireball;
    public Transform shotPoint;
    public float manaRequired = 10f;
    private float attackSpeed = 20.0f;

    private Mana playerMana;

    private int mode = 1;
    int isAttackingHash;

    public float turnTime = 0.1f;
    float turnVelocity;
    public Transform mainCam;
    public CinemachineVirtualCamera secondCam;
    public AudioSource attackAudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMana = GameObject.Find("Player").GetComponent<Mana>();
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            switchMode();
        }

        if (mode == 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerMana.ConsumeMana(manaRequired))
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
            }
        }
        else if (mode == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerMana.ConsumeMana(manaRequired))
            {

                Vector3 point = mainCam.GetComponent<SelectArea>().hit.point;
                Debug.Log(point);

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
            }
            else if (mode == 1)
            {
                GetComponent<PlayerMovement>().enabled = false;
                transform.parent.gameObject.GetComponent<CharacterSwitch>().enabled = false;
            }
    }
}
