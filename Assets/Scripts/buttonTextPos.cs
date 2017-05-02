using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTextPos : MonoBehaviour {

    public float xPos;
    public float yPos;

    private void Awake()
    {
        this.GetComponent<Transform>().position = new Vector2(xPos * Screen.width, yPos * Screen.height);
        this.GetComponent<Transform>().localScale = new Vector2(Screen.width / 1061f, Screen.height / 597f);
    }

    // Use this for initialization

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
