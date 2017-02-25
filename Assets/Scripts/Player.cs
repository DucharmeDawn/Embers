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
    float waterShootTime = 0.25f;

    public GameObject water;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
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