using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

    string sceneName;

	// Use this for initialization
	void Start () {
        sceneName = SceneManager.GetActiveScene().name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            SceneManager.LoadScene("Menu");
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}
