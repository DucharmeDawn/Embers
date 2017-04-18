using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waterMeter : MonoBehaviour {

    public Slider water;
    Player player;


    // Use this for initialization
    void Start()
    {
        water.GetComponent<RectTransform>().localPosition = new Vector2((220 / 274.0f) * (Screen.width / 2.0f) * -1, (140 / 154.0f) * (Screen.height / 2.0f));
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
