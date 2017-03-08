using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Rigidbody2D rb;
	public float groundAcc = 10;
    //public float maxVerticalAcc = 
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
    States currState; 

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
        currState = new Ground(this);
	}

    public bool isGrounded()
    {
        return grounded;
    }

	// Update is called once per frame
	void Update () {
        jump = Input.GetButton("Jump");
        if (jump && !currState.name().Equals("Jump"))
        {
            currState = new Jump(this);
        }
        currState.Update();
        if (Input.GetButton("Fire1") && waterTime > waterShootTime)
        {
            //Debug.Log("Player" + rb.position.x + ", " + rb.position.y);
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
            currState = new Ground(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            currState = new Jump(this);
        }
    }

    private void FixedUpdate()
	{
        currState.FixedUpdate();
	}

    private class Ground : States
    {
        public String myName = "Ground";
        Player self;
        Rigidbody2D rb;
        float groundAcc;
        float jumpForce;
        float horizontalForce;
        float jumpingForce;
        float jumpTime;
        bool grounded;
        bool jump;
        bool crazy = false;

        public Ground(Player self)
        {
            this.self = self;
            this.rb = self.rb;
            this.groundAcc = self.groundAcc;
            this.jumpForce = self.jumpForce;
            this.horizontalForce = self.horizontalForce;
            this.jumpingForce = self.jumpingForce;
            this.jumpTime = self.jumpTime;
            this.grounded = self.grounded;
            this.jump = self.jump;
            this.crazy = self.crazy;
        }

        public string name()
        {
            return myName;
        }

        public void Start()
        {
            horizontalForce = Input.GetAxis("Horizontal");
        }

        public void Update()
        {

        }

        public void FixedUpdate()
        {
            horizontalForce = Input.GetAxis("Horizontal");
            float y = rb.velocity.y;
            rb.velocity = new Vector2(groundAcc * horizontalForce, y);
        }
    }

    public class Jump : States
    {
        public String myName = "Jump";
        Player self;
        Rigidbody2D rb;
        float groundAcc;
        float jumpForce;
        float horizontalForce;
        float jumpingForce;
        float jumpTime;
        bool grounded;
        bool jump;
        bool crazy = false;

        public Jump(Player self) {
            this.self = self;
            this.rb = self.rb;
            this.groundAcc = self.groundAcc;
            this.jumpForce = self.jumpForce;
            this.horizontalForce = self.horizontalForce;
            this.jumpingForce = self.jumpingForce;
            this.jumpTime = self.jumpTime;
            this.grounded = self.grounded;
            this.jump = self.jump;
            this.crazy = self.crazy;
        }

        public string name()
        {
            return myName;
        }

        public void Update()
        {
            jump = Input.GetButton("Jump");
        }

        public void FixedUpdate()
        {
            horizontalForce = Input.GetAxis("Horizontal");
            //float y = rb.velocity.y;
            jumpingForce = Input.GetAxis("Jump");
            //rb.velocity = new Vector2(groundAcc * horizontalForce, y);
            if (jumpTime > 0 && jump)
            {
                crazy = true;
                grounded = false;
                float x = rb.velocity.x;
                rb.velocity = new Vector2(x, jumpForce);
                jumpTime -= Time.deltaTime;
            }
            else
            {
                if (crazy)
                {
                    crazy = false;
                    rb.velocity = new Vector2(groundAcc * horizontalForce, 0);
                }
                else if (!(grounded))
                {
                    rb.velocity = new Vector2(groundAcc * horizontalForce, rb.velocity.y);
                }
            }
        }
    }
}