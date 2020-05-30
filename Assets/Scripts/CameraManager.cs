using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject player;
    private bool cameraPosLocked;
    private bool cameraTargetPlayer;

    private Transform newPosition;

    private float cameraMoveSpeed;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cameraPosLocked = false;
    }

    /// <summary>
    /// TODO: add array waypoint support to MoveCamera();
    /// Add ability to set the cameras 'look' position either on final waypoint or each iteration
    /// </summary>

    private void Update()
    {
        if (cameraPosLocked == false) {
            float step = cameraMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPosition.position, step);
            if (transform.position == newPosition.position) {
                cameraPosLocked = true;
            }
        }
        if (cameraPosLocked && cameraTargetPlayer) {
            TargetPlayer();
        }
    }

    public void MoveCamera(Transform newPos, float moveSpeed, bool targetPlayer)
    {
        cameraPosLocked = false;
        newPosition = newPos;
        cameraTargetPlayer = targetPlayer;
        cameraMoveSpeed = moveSpeed;
    }

    private void TargetPlayer()
    {
        transform.LookAt(player.transform);
    }
}
