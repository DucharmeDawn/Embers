using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPos : MonoBehaviour {

    public float xPos;
    public float yPos;

    private void Awake()
    {
        this.GetComponent<Transform>().position = new Vector2(xPos * Screen.width, yPos * Screen.height);
        this.GetComponent<Transform>().localScale = new Vector2(Screen.width / 212.2f, Screen.height / 119.4f);
    }

    // Use this for initialization
    void Start () {
        //Debug.Log("x = " + transform.position.x + " y = " + transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
