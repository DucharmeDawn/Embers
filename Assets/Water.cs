using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    GameObject player;
    Rigidbody2D playerRB;

    Rigidbody2D rb;

    public float waterForce = 750;

    float life = 1f;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 screenPlayer = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(playerRB.position);
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouse -= screenPlayer;
        rb.velocity = mouse.normalized * 27;
	}
	
	// Update is called once per frame
	void Update () {
        if (life < 0)
        {
            GameObject.Destroy(this.gameObject);
        }
        life -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        


        if (collision.gameObject.name != "Player")
        {
            Vector2 forceVec = -1 * (rb.position - playerRB.position);
            float dist = forceVec.magnitude;
            forceVec = -1 * (rb.position - playerRB.position).normalized;
            forceVec.x *= 2f;
            forceVec.y *= 0.5f;
            if (player.GetComponent<Player>().isGrounded())
            {
                playerRB.AddForce(new Vector2(0, forceVec.y * waterForce / (dist * dist)));
            } else {
                playerRB.AddForce(forceVec * waterForce / (dist * dist));
            }
            Debug.Log("kill");
            GameObject.Destroy(this.gameObject);
        }
    }
}
