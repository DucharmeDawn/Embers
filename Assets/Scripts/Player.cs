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

    float waterTime = 0.5f;
    float waterShootTime = 0.025f;//amount of time between each water particle

    public GameObject water;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

    public bool isGrounded()
    {
        return grounded;
    }

	// Update is called once per frame
	void Update () {
        jump = Input.GetButton("Jump");
        if (Input.GetButton("Fire1") && waterTime > waterShootTime)
        {
            Debug.Log("Player" + rb.position.x + ", " + rb.position.y);
            //GameObject water = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Instantiate<GameObject>(water, rb.transform.position, Quaternion.identity);
            waterTime = 0;
        }
        waterTime += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void FixedUpdate()
	{
        Debug.Log(grounded);
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

    private class Grounded : Player
    {
        Player self;

    }
}