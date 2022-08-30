using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isPlatform, isPlayer;
    [SerializeField] Animator enemyAnim;

    public int minDist;
    public int maxDist;
    public int moveSpeed;

    public int maxHealth;
    public int currentHealth;

    //patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    [Tooltip("Use 'ThirdPersonController' Script")]
    private int animSpeed;
    public float speed;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public bool attacking = false;

    // Awake is called before Start
    private void Awake(){
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animSpeed = Animator.StringToHash("Speed");
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange =  Physics.CheckSphere(transform.position, attackRange, isPlayer);

        transform.LookAt(player);
        if(Vector3.Distance(transform.position, player.position) >= minDist){

            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            if(Vector3.Distance(transform.position, player.position) <= maxDist){

                if(!playerInSightRange && !playerInAttackRange){
                    Patroling();
                    speed = 2.0f;
                    enemyAnim.SetFloat(animSpeed, speed);
                }
                if(playerInSightRange && !playerInAttackRange){
                    ChasePlayer();
                    Debug.Log("In sight");
                    speed = 5.335f;
                    enemyAnim.SetFloat(animSpeed, speed);
                }
                if(playerInSightRange && playerInAttackRange){
                    AttackPlayer();
                    attacking = true;
                    Debug.Log("attack");
                    speed = 5.335f;
                    enemyAnim.SetFloat(animSpeed, speed);
                    
                }
            }
        }  
    }
    //TODO: Make enemies take damage
    void TakeDamage(int damage){
        currentHealth -= damage;
    }

    private void Patroling(){
        if (!walkPointSet){
            SearchWalkPoint();
        }
        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint(){
        float randomX = Random.Range(-31f, 43f);
        float randomZ = Random.Range(-36f, 9f);

        walkPoint = new Vector3(transform.position.x + randomX, 5.7f, transform.position.z + randomZ);

        //if outside map
        if(Physics.Raycast(walkPoint, -transform.up, 2f, isPlatform)){
            walkPointSet = true;
        }
    }

    private void ChasePlayer(){
        agent.SetDestination(player.position);
        
    }
    private void AttackPlayer(){
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if(!alreadyAttacked){
            //TODO: attack code
            //if enemy collides with player, destroy/damage player, restart level
            /*
            if(player.isReached && gameOver){
                Destroy(player);
            }
            */

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack(){
        alreadyAttacked = false;

    }
}
