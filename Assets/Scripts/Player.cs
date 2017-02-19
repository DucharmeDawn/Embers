using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody2D rb;
	float groundAcc = 10;
	float jumpForce = 20;
	float horizontalForce;
	float jumpingForce;
	float jumpTime = .05f;
	bool grounded;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

	}

	private void onColliderEnter2D(Collider2D col){
		if (col.name == ("ground")) {
			grounded = true;
		}
	}
	private void FixedUpdate()
	{
		horizontalForce = Input.GetAxis("Horizontal");
		float y = rb.velocity.y;
		jumpingForce = Input.GetAxis("Jump");
		//rb.AddForce(new Vector2(groundAcc * horizontalForce, 0));
		rb.velocity = new Vector2(groundAcc * horizontalForce, y);
		grounded = (jumpingForce == 0);
		if (jumpTime > 0) {
			if (Input.GetButton("Jump")) {
				grounded = false;
				float x = rb.velocity.x;
				rb.velocity = new Vector2 (x, jumpForce);
				jumpTime -= Time.deltaTime;
			}
		} else {
			rb.velocity = new Vector2 (groundAcc * horizontalForce, 0);
		}	
		if (grounded) {
			jumpTime = .05f;
		}
	}
}