using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private CameraManager cm;
    private bool areaTriggered;
    public Transform[] newCameraLocations;
    public float cameraMoveSpeed = 2f;
    public bool targetPlayer;
    
    void Awake()
    {
        areaTriggered = false;
        cm = GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!areaTriggered)
        {
            if (other.tag == "Player")
            {
                cm.MoveCamera(newCameraLocations, cameraMoveSpeed, targetPlayer);
                areaTriggered = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            areaTriggered = false;
        }


    }
}
