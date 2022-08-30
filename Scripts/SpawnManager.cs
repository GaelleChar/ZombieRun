using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    public GameObject obstacle;

    private PlayerController playerControllerScript;

    private float startDelay = 2;
    private float repeatRate = 2;
    private int enemyCounter = 0;
    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObstacle(){
        float randomX = Random.Range(-31f, 43f);
        float randomZ = Random.Range(-36f, 9f);
        
        spawnPos = new Vector3(randomX, 5.8f, randomZ);

        if(playerControllerScript.gameOver == false && enemyCounter < 3){
            Instantiate(obstacle, spawnPos, obstacle.transform.rotation);
            enemyCounter ++;
        }
        else{
            return;
        }
    }
    

}
