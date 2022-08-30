using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;
    public bool isOnPlatform = true;
    public float jumpForce;
    public float gravityModifier;

    public int maxHealth;
    public int currentHealth;

    public ParticleSystem explosion;
    public GameObject player;
    private Rigidbody playerRb;
    private Animator playerAnim;
    public Enemy enemyScript;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<GameObject>();
        enemyScript = GetComponent<Enemy>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnPlatform && !gameOver){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnPlatform = false;
        }
        /*if(enemyScript.attacking == true){
            TakeDamage(20);
        }
        */
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Platform")){
            isOnPlatform = true;
        }
        else if(collision.gameObject.CompareTag("Obstacle")){
            Debug.Log("Game Over");
            gameOver = true;
            //FIXME: change anim
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
        }
        else if(collision.gameObject.CompareTag("Enemy") ){
            Debug.Log("taking damage");
            TakeDamage(20);
        }
    }
    void TakeDamage(int damage){
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            if(currentHealth == 0){
                //explosion.Play();
                Debug.Log("dead");
                
                //Destroy(gameObject);
            }
            if(currentHealth <= 0){
                currentHealth = 0;
            }

        Debug.Log(currentHealth);
    }

}
