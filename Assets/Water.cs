using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    GameObject player;
    Rigidbody2D playerRB;

    Rigidbody2D rb;

    float waterForce = 5000;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 screenPlayer = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(playerRB.position);
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Debug.Log(mouse);
        mouse -= screenPlayer;
        Debug.Log(mouse);
        rb.velocity = mouse.normalized * 27;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(rb.position.x + ", " + rb.position.y);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        


        if (collision.gameObject.name != "Player")
        {
            Vector2 forceVec = -1 * (rb.position - playerRB.position);
            float dist = forceVec.magnitude;
            playerRB.AddForce(forceVec.normalized * (1 / dist) * waterForce);
            Debug.Log("kill");
            GameObject.Destroy(this.gameObject);
        }
    }
}
