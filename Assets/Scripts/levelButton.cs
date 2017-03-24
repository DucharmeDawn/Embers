using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelButton : MonoBehaviour {

    //public int x;
    //public Button yourButton;

    //void Start()
    //{
    //    Button btn = yourButton.GetComponent<Button>();
    //    btn.onClick.AddListener(TaskOnClick);
    //}

    //public void TaskOnClick()
    //{
    //    Debug.Log("You have clicked the button!");
    //    SceneManager.LoadScene("Level" + x.ToString());
    //}

    string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    public void goToLevel(int x)
    {
        Debug.Log("adsf");
        SceneManager.LoadScene("Level" + x.ToString());
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
