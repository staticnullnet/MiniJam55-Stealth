﻿using SA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] GameObject gate;
    bool levelLoading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //InputHandler inputHandler = other.gameObject.GetComponent<InputHandler>();
            //inputHandler.freezeMovement = true;

            // TODO: Make gate move up
            //gate.transform.Translate(Vector3.up * 50 * Time.deltaTime, Space.World);
            gate.SetActive(false);
            if (!levelLoading)
            {
                levelLoading = true;
                Invoke("LoadFirstLevel", loadLevelDelay);
            }
        }
    }

    private void LoadFirstLevel()
    {        
        Debug.Log("Win!"); Debug.Log("Loading Level");
        SceneManager.LoadSceneAsync("WinScreen");
    }
}
