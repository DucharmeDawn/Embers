using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{

    GameObject[] buttons;

    // Use this for initialization
    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
    }

    public void alive()
    {
        //GameObject.Find("embersAlt").GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        for (int x = 0; x < buttons.Length; x++)
        {
            buttons[x].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") || Input.GetKey("escape"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            for (int x = 0; x < buttons.Length; x++)
            {
                buttons[x].SetActive(true);
            }
            //GameObject.Find("embersAlt").GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
