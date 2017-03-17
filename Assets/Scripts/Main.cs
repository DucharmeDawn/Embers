﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    GameObject player;
    float bottomLimit;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        Camera myCam = GetComponent<Camera>();
        Vector3 cameraBottomLeft = myCam.ScreenToWorldPoint(new Vector3(0, 0));
        bottomLimit = gameObject.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 myPos = gameObject.transform.position;
        this.gameObject.transform.position = new Vector3(player.transform.position.x, Math.Max(bottomLimit, player.transform.position.y), myPos.z);
	}
}
