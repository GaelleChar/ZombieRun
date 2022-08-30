using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    //TODO: when player shoots, kill
    public bool isOnPlatform = true;
    public bool playerTouched;
    public float speed;

    private Rigidbody enemyRb;

    private GameObject player;

    //private Animator enemyAnim;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        //randomly move up and down
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);

        enemyRb.MovePosition(lookDirection);
        transform.LookAt(player.transform);

        //enemyAnim.SetTrigger("Right_trig");

    }
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("playerTouched");
            playerTouched = true;
            //TODO: lower player health
        }
    }
    
}
