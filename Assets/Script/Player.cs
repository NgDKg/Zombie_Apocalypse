using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player movement")]
    public float playerSpeed = 1.9f;
    public float playerSprint = 3f;

    [Header("Player Health Things")]
    private float playerHealth = 120f;
    public float presentHealth;
    public GameObject playerDamage;
    public HealthBar healthBar;

    [Header("Player Camera")]
    public Transform playerCam;
    public GameObject EndGameMenuUI;

    [Header("Player Animator and Gravity")]
    public CharacterController characterController;
    public float gravity = -9.81f;
    public Animator animator;


    [Header("Player Jumping and Vilociy")]
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surFaceDistance = 0.4f;
    public LayerMask surfaceMask;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth = playerHealth;
        healthBar.GiveFullHeal(playerHealth);
    }
    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surFaceDistance, surfaceMask);
        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        playerMove();
        jump();
        sprint();
    }

    void playerMove ()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("IdleAim", false);


            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
        }
    }

    void jump()
    {
        if(Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");

            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
    }

    void sprint()
    {
        if(Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Running", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Wlak", true);
                animator.SetBool("Running", false);
            }
                
        }
    }

    public void playerHitDamage(float takeDamge)
    {
        presentHealth -= takeDamge;
        StartCoroutine(PlayerDamageEffect());

        healthBar.SetHealth(presentHealth);

        if (presentHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void PlayerDie()
    {
        SceneManager.LoadScene("Endgame2");
    }

    IEnumerator PlayerDamageEffect()
    {
        playerDamage.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        playerDamage.SetActive(false);
    }
}
