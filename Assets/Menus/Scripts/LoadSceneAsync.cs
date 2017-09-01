using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAsync : MonoBehaviour {

    public string sceneName;

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
        LoadAsync();
    }

    public void LoadAsync()
    {
        //SceneManager.
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
    }
}
