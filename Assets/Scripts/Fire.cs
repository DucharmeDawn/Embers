using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    static float startSize = 0.5f;
    float size;
    int health = 5;
    int hits;
    float time;
    float respawnTime = 3;
    SpriteRenderer sr;
    BoxCollider2D bc;
    Rigidbody2D rb;
    BoxCollider2D selfBC;
    Transform tr;
    Vector3 pos;

    public GameObject lit;


	// Use this for initialization
	void Start ()
    {
        selfBC = gameObject.GetComponent<BoxCollider2D>();
        hits = health;
        size = startSize;
        rb = lit.GetComponent<Rigidbody2D>();
        sr = lit.GetComponent<SpriteRenderer>();
        bc = lit.GetComponent<BoxCollider2D>();
        tr = lit.GetComponent<Transform>();
        pos = tr.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Water"))
        {
            if (hits == 0)
            {
                selfBC.enabled = false;
                //gameObject.GetComponent<SpriteRenderer>().enabled = false;
                sr.enabled = false;
                //bc.isTrigger = true;
                bc.enabled = false;
                rb.gravityScale = 0;
            }
             else if (hits > 0)
            {
                size -= 0.1f;
                hits -= 1;
                lit.GetComponent<Transform>().localScale = new Vector3(size, size);
                //gameObject.GetComponent<SpriteRenderer>().transform.localScale = new Vector3(size, size);
            }
            //else
            //{
            //    sr.enabled = false;
            //    bc.enabled = false;
            //    size = 0;
            //}
        }
    }

    // Update is called once per frame
    void Update () {
		if (hits == 0)
        {
            time += Time.deltaTime;
            if (time > respawnTime)
            {
                size = startSize;
                hits = health;
                lit.GetComponent<Transform>().localScale = new Vector3(size, size);
                tr.position = pos;
                //bc.isTrigger = false;
                time = 0;
                sr.enabled = true;
                bc.enabled = true;
                rb.gravityScale = 1;
                selfBC.enabled = true;
            }
        }
	}

    public int life()
    {
        return hits;
    }
}
