using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterParticles : MonoBehaviour {

    public GameObject arms;
    Transform armsTrans;
    GameObject[] water;
    ParticleSystem ps;
    GameObject player;
    Rigidbody2D playerRB;

	// Use this for initialization
	void Start () {
        armsTrans = arms.GetComponent<Transform>();
        ps = gameObject.GetComponent<ParticleSystem>();
        player = GameObject.Find("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        water = GameObject.FindGameObjectsWithTag("Water");
    }
	
	// Update is called once per frame
	void Update () {
        var em = ps.emission;
        if (water.Length != 0)
        {
            em.enabled = true;
        } else
        {
            em.enabled = false;
        }
        float zPos = armsTrans.eulerAngles.z;
        Vector2 screenPlayer = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(playerRB.position);
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        mouse -= screenPlayer;
        float yPos = Mathf.Abs(gameObject.transform.eulerAngles.y);
        if (mouse.x < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(zPos, -90, gameObject.transform.eulerAngles.z);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(-zPos, 90, gameObject.transform.eulerAngles.z);
        }
	}

    private void FixedUpdate()
    {
        water = GameObject.FindGameObjectsWithTag("Water");
    }
}
