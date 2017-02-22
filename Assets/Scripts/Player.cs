using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody2D rb;
	float groundAcc = 10;
	float jumpForce = 20;
	float horizontalForce;
	float jumpingForce;
	float jumpTime = .25f;
	bool grounded;
    bool jump;
    bool crazy = false;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
        jump = Input.GetButton("Jump");
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == ("ground"))
        {
            grounded = true;
            Debug.Log(grounded);
        }
    }

    private void FixedUpdate()
	{
		horizontalForce = Input.GetAxis("Horizontal");
		float y = rb.velocity.y;
		jumpingForce = Input.GetAxis("Jump");
		//rb.AddForce(new Vector2(groundAcc * horizontalForce, 0));
		rb.velocity = new Vector2(groundAcc * horizontalForce, y);
		if (jumpTime > 0 && jump) {
                crazy = true;
				grounded = false;
				float x = rb.velocity.x;
				rb.velocity = new Vector2 (x, jumpForce);
				jumpTime -= Time.deltaTime;
                Debug.Log("jumped");
		} else {
            if (crazy)
            {
                crazy = false;
                rb.velocity = new Vector2(groundAcc * horizontalForce, 0);
            } else if (!(grounded))
            {
                rb.velocity = new Vector2(groundAcc * horizontalForce, rb.velocity.y);
            }
        }	
		if (grounded) {
			jumpTime = .25f;
		}
	}
}