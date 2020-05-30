using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private float levelLoadDelay = 5f;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void LoadNextLevel()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        int nextBuildIndex = currentBuildIndex + 1;

        //if next build index is past the level count, go back to level 1
        if (currentBuildIndex == SceneManager.sceneCountInBuildSettings - 1)
            nextBuildIndex = 0;
        else
            nextBuildIndex = currentBuildIndex + 1;


        SceneManager.LoadScene(nextBuildIndex);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("LoadFirstLevel", levelLoadDelay);
    }
}
