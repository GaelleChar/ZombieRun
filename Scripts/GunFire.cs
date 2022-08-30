using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    [SerializeField] ParticleSystem bullet;
    public Camera firstPersonCamera;
    public Camera defaultCamera;
    public bool gunClicked;

   public enum State {gunEquipped, gunNotEquipped}; 
   State state;

    // Update is called once per frame
    void Update()
    {
        DoGunNotEqipped();
        CheckStates();
    }

    public void ShootBullet(){
        bullet.Play();
    }

    void CheckStates(){
        switch(state)
        {
            case State.gunNotEquipped:
                DoGunNotEqipped();
                break;

            case State.gunEquipped:
                if(Input.GetKeyDown(KeyCode.E)){
                    StartCoroutine(DoGunEqipped());
                }
                break;

            default:
                break;

        }

    }
        
    IEnumerator DoGunEqipped(){
        
        defaultCamera.enabled = false;
        firstPersonCamera.enabled = true;
        gunClicked = true;

        while(firstPersonCamera.enabled == true){

            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.G)){
                ShootBullet();
                Debug.Log("Shooting");
            }

            Debug.Log("Shoot with right click");
            
        }
        yield return new WaitForSeconds(45f);
        
    }

    void DoGunNotEqipped(){
        firstPersonCamera.enabled = false;
        defaultCamera.enabled = true;
        gunClicked = false;

    }
}
 

 