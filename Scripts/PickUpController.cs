using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public ProjectileGun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float PickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;
    // Start is called before the first frame update
    void Start()
    {
        //setup
        if(equipped){
            gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
        if(!equipped){
            gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= PickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull){
            PickUp();
        }
        if(equipped && Input.GetKeyDown(KeyCode.Q)){
            Drop();
        }
    }

    private void PickUp(){
        Debug.Log("equipped");
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //trigger kinmatic and box collider
        rb.isKinematic = true;
        coll.isTrigger = true;

        //enable script
        gunScript.enabled = true;
    }
    private void Drop(){
        Debug.Log("not equipped");
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //trigger kinmatic and box collider
        rb.isKinematic = false;
        coll.isTrigger = false;

        //gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //add force
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        //enable script
        gunScript.enabled = false;

    }
}
