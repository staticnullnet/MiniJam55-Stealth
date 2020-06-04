using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 8f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadMainMenu", loadLevelDelay);
    }

    private void LoadMainMenu()
    {
        Debug.Log("Loading Level");
        SceneManager.LoadSceneAsync("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
