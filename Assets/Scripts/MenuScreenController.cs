using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void LoadNextScene()
    {
        //SceneManager.LoadSceneAsync("IntroText");
        SceneManager.LoadSceneAsync("castle_intro");

        //if (Input.GetMouseButtonDown(0))
        //{
        //    SceneManager.LoadSceneAsync("IntroText");
        //}
    }
}
