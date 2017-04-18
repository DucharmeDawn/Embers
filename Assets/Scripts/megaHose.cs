using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class megaHose : MonoBehaviour {

    public Image hose;

	// Use this for initialization
	void Start () {
        hose.GetComponent<RectTransform>().localPosition = new Vector2((-250 / 274.0f) * (Screen.width/2.0f), (125 / 154.0f) * (Screen.height/2.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
