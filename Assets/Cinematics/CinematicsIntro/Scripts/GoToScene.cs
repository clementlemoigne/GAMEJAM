using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {

    public GameObject[] objects;
    public bool useGoToSceneOffset;
    public string sceneName;
    public float timer;
    public LoadSceneAsync loadSceneAsync;
    void Start()
    {
        if(useGoToSceneOffset)
        {
            StartCoroutine(GoToSceneIn(timer, sceneName));
        }
    }

    IEnumerator GoToSceneIn(float timer, string name)
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(name);
    }

    public void GoTo(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void FindGameObjectInNextScene()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
        GameObject.Find("CinematiqueIntro").GetComponent<GoToScene>().ActivateAssetInNewScene();
    }

    public void ActivateAssetInNewScene()
    {
        for(int i = 0; i < objects.Length; i++ )
        {
            objects[i].SetActive(true);
        }
        SceneManager.UnloadSceneAsync("Menu");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Intro"));

    }

    public void GoToIn(float timer)
    {
        StartCoroutine(GoToSceneIn(timer, sceneName));
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("BonusOutRun1", 500);
        PlayerPrefs.SetInt("BonusOutRun2", 500);
        PlayerPrefs.SetInt("BonusBS", 500);
        PlayerPrefs.SetInt("BonusBG", 500);

    }

}
