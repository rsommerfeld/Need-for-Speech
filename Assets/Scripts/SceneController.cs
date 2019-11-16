using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
	// Use this for initialization
	public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
