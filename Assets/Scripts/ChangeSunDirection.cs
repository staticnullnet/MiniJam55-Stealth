using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSunDirection : MonoBehaviour
{
    public Light sun;
    public Transform newSun;
    public float sunTransitionSpeed = 3f;
    private Quaternion initalSunDirection;
    private Quaternion secondSunDirection;

    private bool changeSun = false;

    private void Awake()
    {
        initalSunDirection = sun.transform.rotation;
        secondSunDirection = newSun.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * TODO: re-evaluate with after a rest */

        float step = sunTransitionSpeed * Time.deltaTime;
        if (!changeSun && sun.transform.rotation != initalSunDirection)
        {
            Quaternion targetRotation = initalSunDirection;
            sun.transform.rotation = Quaternion.RotateTowards(sun.transform.rotation, targetRotation, sunTransitionSpeed * Time.deltaTime);
        }
        else
        {
            if (!changeSun && sun.transform.rotation == initalSunDirection)
            {
                changeSun = !changeSun;
            }
        }
        if (changeSun && sun.transform.rotation != secondSunDirection) {

            Quaternion targetRotation = secondSunDirection;
            sun.transform.rotation = Quaternion.RotateTowards(sun.transform.rotation, targetRotation, sunTransitionSpeed * Time.deltaTime);
        } else {
            if (changeSun && sun.transform.rotation == secondSunDirection)
            {
                changeSun = !changeSun;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            changeSun = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            changeSun = true;


        }
    }

}
