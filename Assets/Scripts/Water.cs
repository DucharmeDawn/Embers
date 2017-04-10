using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

    GameObject player;
    Rigidbody2D playerRB;

    Rigidbody2D rb;

    public float waterForce = 825;

    float life = 1f;

    public bool megaHose;
    float megaWaterForce = 10000;
    float megaLife = 0.05f;


	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 screenPlayer = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(playerRB.position);
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouse -= screenPlayer;
        rb.velocity = mouse.normalized * 27;
        if (megaHose)
        {
            life = megaLife;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (life < 0)
        {
            GameObject.Destroy(this.gameObject);
        }
        int x = 1;
        if (megaHose)
        {
            while (x < 10)
            {
                calcForce(megaWaterForce * 7 * (1 / (Mathf.Pow(2, 2 * x))));
                x += 1;
            }
        }
        life -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag.Equals("Hazard")) {
        //    Destroy(collision.gameObject);
        //}
        
        if (collision.gameObject.name != "Player")
        {
            if (megaHose)
            {
                calcForce(megaWaterForce);
            } else
            {
                calcForce(waterForce);
            }
            GameObject.Destroy(this.gameObject);
        }
    }

    private void calcForce(float x)
    {
        Vector2 forceVec = -1 * (rb.position - playerRB.position);
        float dist = forceVec.magnitude;
        forceVec = -1 * (rb.position - playerRB.position).normalized;
        forceVec.x *= 2f;
        forceVec.y *= 0.5f;
        if (player.GetComponent<Player>().isGrounded())
        {
            playerRB.AddForce(new Vector2(0, forceVec.y * x / ((dist * dist) + 1)));
        }
        else
        {
            playerRB.AddForce(forceVec * x / ((dist * dist) + 1));
        }
    }
}
