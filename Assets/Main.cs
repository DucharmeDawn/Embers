using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        Camera myCam = GetComponent<Camera>();
        Vector3 cameraBottomLeft = myCam.ScreenToWorldPoint(new Vector3(0, 0));
    }
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform. = player.transform.position.x;

	}
}
