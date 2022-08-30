using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseSensivity = 100f;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensivity, 0, 0);
        player.transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensivity, 0);
        
    }
}
