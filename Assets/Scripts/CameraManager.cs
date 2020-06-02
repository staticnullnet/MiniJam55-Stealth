using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject player;
    private bool cameraPosLocked;
    private bool cameraTargetPlayer;
    private int currentCameraWaypoint = 0;
    private float cameraTurningRate = 30f;

    //the direct next movement
    private Transform newPosition;
    //all movements the camera has to make
    private Transform[] newPositions;
    private Transform lastPosition;

    private GameObject lastTrigger = null;

    private float cameraMoveSpeed;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cameraPosLocked = false;
    }

    /// <summary>
    /// TODO: potentially add the ability to transition the waypoints of the current trigger in reverse if the player goes backwards to the previous trigger
    /// </summary>

    private void Update()
    {
        if (cameraPosLocked == false) {

            float step = cameraMoveSpeed * Time.deltaTime;
            if (newPosition != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition.position, step);

                Quaternion targetRotation = newPosition.transform.rotation;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, cameraTurningRate * Time.deltaTime);

                if (transform.position == newPosition.position)
                {
                    if (transform.position == lastPosition.position && newPositions.Length == currentCameraWaypoint)
                    {
                        cameraPosLocked = true;
                        newPosition = null;
                        currentCameraWaypoint = 0;
                    }
                    else
                    {
                        if (newPositions.Length != currentCameraWaypoint && currentCameraWaypoint <= newPositions.Length)
                        {
                            currentCameraWaypoint += 1;
                            //Debug.Log("current waypoint: " + currentCameraWaypoint + " current positions array length: " + newPositions.Length);
                        }
                    }

                }
                if (newPositions.Length > 1)
                {
                    newPosition = newPositions[currentCameraWaypoint];
                }
            }
        }
        if (cameraPosLocked && cameraTargetPlayer) {
            TargetPlayer();
        }
    }

    public void MoveCamera(Transform[] newPos, float moveSpeed, bool targetPlayer, GameObject cameraTrigger)
    {
        cameraPosLocked = false;

        if (cameraTrigger == lastTrigger)
            System.Array.Reverse(newPos);

        newPositions = newPos;
        newPosition = newPos[0];
        lastPosition = newPos[newPos.Length - 1];
        cameraTargetPlayer = targetPlayer;
        cameraMoveSpeed = moveSpeed;

        lastTrigger = cameraTrigger;

        
    }

    private void TargetPlayer()
    {
        transform.LookAt(player.transform);
    }


}
