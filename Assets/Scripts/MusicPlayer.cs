using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{    
    private static MusicPlayer musicPlayer;

    void Awake()
    {
        MakeThisTheOnlyMusicPlayer();
    }
       
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MakeThisTheOnlyMusicPlayer()
    {
        if (musicPlayer == null)
        {
            DontDestroyOnLoad(gameObject);
            musicPlayer = this;
        }
        else
        {
            if (musicPlayer != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
