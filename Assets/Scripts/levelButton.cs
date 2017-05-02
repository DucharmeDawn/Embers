using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelButton : MonoBehaviour {

    string sceneName;
    public float xPos;
    public float yPos;

    private void Awake()
    {
        this.GetComponent<Transform>().position = new Vector2(xPos * Screen.width, yPos * Screen.height);
        this.GetComponent<Transform>().localScale = new Vector2(Screen.width / 265.25f, Screen.height / 149.25f);
    }

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    public void goToLevel(int x)
    {
        SceneManager.LoadScene("Level" + x.ToString());
    }
}
