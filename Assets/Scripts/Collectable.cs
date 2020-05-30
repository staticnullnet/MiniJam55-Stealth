using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private bool collected = false;
    private GameObject player;

    public bool showLabel;
    public GameObject textLabel;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (textLabel != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {
                textLabel.gameObject.SetActive(true);
            }
            else
            {
                textLabel.gameObject.SetActive(false);
            }
        }
    }

    public void Collect()
    {
        //set condition for 'good' ending, currently only 1 collectable
        collected = true;
        Destroy(this.gameObject);
    }
}
