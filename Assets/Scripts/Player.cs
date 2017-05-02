using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	Rigidbody2D rb;
	public float groundAcc = 16;
    //public float maxVerticalAcc = 
    float maxAirHorizontalAcc = 1;
	float jumpForce = 15;
	float horizontalForce;
	float jumpingForce;
	float jumpTime = .15f;
	bool grounded;
    bool jump;
    bool crazy = false;
    String scene;
    float totalWater = 100;
    public float waterCount;
    int megaHoseInt;
    float timer;
    float deltaTimer;

    float waterTime = 0.5f;

    //float waterShootTime = 1f;//amount of time between each water particle

    float waterShootTime = 0.025f;//amount of time between each water particle

    public GameObject water;
    public GameObject megaHose;
    public Slider waterMeter;
    States currState;
    
    public AudioSource aud;

    private void Awake()
    {
        aud.enabled = true;
    }

    private void OnGUI()
    {
        //GUI.Label(new Rect(0, 0, Screen.width, Screen.height), waterCount.ToString());
        GUI.color = Color.black;
        GUI.Label(new Rect(Screen.width * 0.06f, Screen.height * 0.075f, 50, 50), " x " + megaHoseInt.ToString());
        GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.13f, 100, 100), timer.ToString());
    }

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        currState = new Ground(this);
        scene = SceneManager.GetActiveScene().name;
        waterCount = totalWater;
        timer = 0f;
        aud.enabled = true;
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
        jump = Input.GetButtonDown("Jump");
        currState.Update();
        if (jump && !currState.name().Equals("Jump"))
        {
            currState = new Jump(this);
        }
        if (Input.GetButton("Fire1") && waterTime > waterShootTime)
        {
            if (megaHoseInt > 0)
            {
                Instantiate<GameObject>(megaHose, rb.transform.position, Quaternion.identity);
                megaHoseInt -= 1;
                waterTime = -1;
            }
            else if (waterCount > 0)
            {
                Instantiate<GameObject>(water, rb.transform.position, Quaternion.identity);
                waterTime = 0;
                waterCount -= 1;
            }
        }
        if (Input.GetButtonDown("Fire1") && waterCount > 0)
        {
            aud.Play();
        }
        if (Input.GetButtonUp("Fire1") || waterCount < 1)
        {
            aud.Stop();
        }
        waterTime += Time.deltaTime;
        if (Input.GetKeyDown("r"))
        {
            currState = new Dead(this);
        }
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Menu");
            SceneManager.UnloadSceneAsync(scene);
        }
        timer = (float) Math.Round(timer + Time.deltaTime, 2);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Hazard"))
        {
            currState = new Dead(this);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Ground"))
        {
            if (currState.name() != "Dead")
            {
                grounded = true;
                currState = new Ground(this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("megaHose"))
        {
            megaHoseInt = 3;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("waterSource"))
        {
            if (totalWater > waterCount)
            {
                waterCount += 1;
            }
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
        waterMeter.value = waterCount;
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
            jumpingForce = Input.GetAxis("Jump");
            if (jumpTime > 0 && jump)
            {
                jump = false;
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
                    float x = groundAcc * horizontalForce;
                    rb.velocity = new Vector2(x, 3);
                }
                else if (!(grounded))
                {
                    float x = groundAcc * horizontalForce;
                    rb.velocity = new Vector2(x, rb.velocity.y);
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