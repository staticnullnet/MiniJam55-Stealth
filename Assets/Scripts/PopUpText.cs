using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpText : MonoBehaviour
{

    public bool showLabel;
    public GameObject textLabel;

    private bool triggered = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (textLabel != null && triggered == false)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            {
                textLabel.gameObject.SetActive(true);
                StartCoroutine(ExampleCoroutine());
                triggered = true;
            }
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
        textLabel.gameObject.SetActive(false);
    }
}
