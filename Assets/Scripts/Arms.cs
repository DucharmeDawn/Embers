using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour {

    GameObject player;
    Rigidbody2D playerRB;
    GameObject arms;
    float zPos;

// Use this for initialization
void Start () {
        player = GameObject.Find("Player");
        arms = GameObject.Find("arms");
        playerRB = player.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        Vector2 screenPlayer = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(playerRB.position);
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouse -= screenPlayer;
        zPos = Mathf.Atan(mouse.normalized.y / mouse.normalized.x) * (180 / Mathf.PI);

        if (mouse.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        //Quaternion target = Quaternion.Euler(0, 0, zPos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime);
        arms.GetComponent<Transform>().transform.eulerAngles = new Vector3(0, 0, zPos);
    }
}
