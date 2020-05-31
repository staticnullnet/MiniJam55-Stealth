using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 8f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("scene loaded. performing invoke...");
        Invoke("LoadFirstLevel", loadLevelDelay);
        Debug.Log("invoked.");
    }

    private void LoadFirstLevel()
    {
        Debug.Log("Loading Level");
        SceneManager.LoadSceneAsync("Scene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
