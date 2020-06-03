using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class EditorOnly : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
        if (EditorApplication.isPlaying || Application.isPlaying)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.enabled = false;
        }        
        else return;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
