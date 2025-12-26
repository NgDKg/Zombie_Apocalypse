using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [Header("Zombie health & Dmg")]
    public float zombieHealth = 100f;
    private float presentHealth;
    public float giveDmg = 5f;
    public HealthBar healthBar;

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Camera AttackingRaycastArena;
    public Transform playerBody;
    public LayerMask playerLayer;

    [Header("Zombie Guarding Var")]
    public GameObject[] walkPoints;
    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingPointRadius = 2f;

    [Header("Zombie Attack Var")]
    public float timeBtwAttack;
    bool previouslyAttack;

    [Header("Zombie Animation")]
    public Animator anim;


    [Header("Zombie mood/states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvisionRadius;
    public bool playerInAttackingRadius;

    private void Awake()
    {
        presentHealth = zombieHealth;
        healthBar.GiveFullHeal(zombieHealth);
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInAttackingRadius = Physics.CheckSphere(transform.position, attackingRadius, playerLayer);

        if (!playerInvisionRadius && !playerInAttackingRadius) Guard();
        if (playerInvisionRadius && !playerInAttackingRadius) PursuePlayer();
        if (playerInvisionRadius && playerInAttackingRadius) AttackPlayer();
    }

    private void Guard()
    {
        if(Vector3.Distance(walkPoints[currentZombiePosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePosition = Random.Range(0, walkPoints.Length);
            if (currentZombiePosition >= walkPoints.Length)
            {
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed);
        transform.LookAt(walkPoints[currentZombiePosition].transform.position);
    }

    private void PursuePlayer()
    {
        if (zombieAgent.SetDestination(playerBody.position)) 
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", true);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", false);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);
        }
    }

    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if(!previouslyAttack)
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(AttackingRaycastArena.transform.position, AttackingRaycastArena.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("Attacking" + hitInfo.transform.name);

                Player playerBody = hitInfo.transform.GetComponent<Player>();

                if(playerBody != null)
                {
                    playerBody.playerHitDamage(giveDmg);       
                }

                anim.SetBool("Walking", false);
                anim.SetBool("Running", false);
                anim.SetBool("Attacking", true);
                anim.SetBool("Died", false);
            }

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }
    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }

    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);
        if (presentHealth <= 0)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            anim.SetBool("Attacking", false);
            anim.SetBool("Died", true);
            zombieDie();
        }    
    }

    private void zombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInAttackingRadius = false;
        playerInvisionRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
