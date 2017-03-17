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
    Transform tr;
    Vector3 pos;


	// Use this for initialization
	void Start ()
    {
        hits = health;
        size = startSize;
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        tr = gameObject.GetComponent<Transform>();
        pos = tr.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Water"))
        {
            if (hits == 0)
            {
                sr.enabled = false;
                //bc.isTrigger = true;
                bc.enabled = false;
            }
             else if (hits > 0)
            {
                Debug.Log(size);
                size -= 0.1f;
                hits -= 1;
                gameObject.GetComponent<Transform>().localScale = new Vector3(size, size);
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
            Debug.Log("0 size");
            time += Time.deltaTime;
            if (time > respawnTime)
            {
                Debug.Log("respawn");
                size = startSize;
                hits = health;
                gameObject.GetComponent<Transform>().localScale = new Vector3(size, size);
                tr.position = pos;
                //bc.isTrigger = false;
                bc.enabled = true;
                time = 0;
                sr.enabled = true;
            }
        }
	}
}
