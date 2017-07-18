using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAsync : MonoBehaviour {
    private AsyncOperation async;
    public string sceneName;
    public GameObject splashScreenBackGround;
    public Animator menuAnimator;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        menuAnimator.speed = 0.0f;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        if (sceneName == "")
            yield break;
        yield return new WaitForSeconds(0.5f);

        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
       

        Debug.Log("start loading");
        while (async.progress <1f)
            yield return null;
        async.allowSceneActivation = false;
        Debug.Log("async loading success");
        splashScreenBackGround.SetActive(false);
        menuAnimator.speed = 1.0f;
    }

    public void PlayGame()
    {
        async.allowSceneActivation = true;
    }

    public void LoadAsync()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
    }
}
