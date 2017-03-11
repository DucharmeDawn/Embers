using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	Rigidbody2D rb;
	public float groundAcc = 10;
    //public float maxVerticalAcc = 
    float maxAirHorizontalAcc = 1;
	float jumpForce = 20;
	float horizontalForce;
	float jumpingForce;
	float jumpTime = .25f;
	bool grounded;
    bool jump;
    bool crazy = false;
    String scene;


    float waterTime = 0.5f;
    float waterShootTime = 0.025f;//amount of time between each water particle

    public GameObject water;
    States currState; 

	// Use this for initialization
	void Start () {

		rb = gameObject.GetComponent<Rigidbody2D>();
        currState = new Ground(this);
        scene = SceneManager.GetActiveScene().name;
        //wait();
    } 

    //IEnumerator wait()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //}

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
            Instantiate<GameObject>(water, rb.transform.position, Quaternion.identity);
            waterTime = 0;
        }
        waterTime += Time.deltaTime;
        if (Input.GetKey("r"))
        {
            currState = new Dead(this);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
            currState = new Ground(this);
        }
        if (col.gameObject.tag.Equals("Hazard"))
        {
            currState = new Dead(this);
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
        float maxAirHorizontalAcc;
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
            this.maxAirHorizontalAcc = self.maxAirHorizontalAcc;
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
                //if (x > maxAirHorizontalAcc)
                //{
                //    rb.velocity = new Vector2(maxAirHorizontalAcc, jumpForce);
                //}
                //else
                //{
                //    rb.velocity = new Vector2(x, jumpForce);
                //}
                jumpTime -= Time.deltaTime;
            }
            else
            {
                if (crazy)
                {
                    crazy = false;
                    float x = groundAcc * horizontalForce;
                    rb.velocity = new Vector2(x, 0);
                    //if (x > maxAirHorizontalAcc)
                    //{
                    //    rb.velocity = new Vector2(maxAirHorizontalAcc, 0);
                    //}
                    //else
                    //{
                    //    rb.velocity = new Vector2(x, 0);
                    //}
                }
                else if (!(grounded))
                {
                    float x = groundAcc * horizontalForce;
                    rb.velocity = new Vector2(x, rb.velocity.y);
                    //if (x > maxAirHorizontalAcc)
                    //{
                    //    rb.velocity = new Vector2(maxAirHorizontalAcc, rb.velocity.y);
                    //}
                    //else
                    //{
                    //    rb.velocity = new Vector2(x, rb.velocity.y);
                    //}
                }
            }
        }
    }

        public class Dead : States
        {
            public String myName = "Dead";
            Player self;
            Rigidbody2D rb;
            float groundAcc;
            float jumpForce;
            float horizontalForce;
            float maxAirHorizontalAcc;
            float jumpingForce;
            float jumpTime;
            bool grounded;
            bool jump;
            bool crazy = false;
            String scene;
            IEnumerator coroutine;

            public Dead(Player self)
            {
                this.scene = self.scene;
            }

            public void FixedUpdate()
            {

            }

            public string name()
            {
                return myName;
            }

            IEnumerator wait()
            {
                yield return new WaitForSeconds(5);
            }

            public void Update()
            {
                wait();
                SceneManager.LoadScene(this.scene);
            }
        }
    }