using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetAndPushLeaderBoardJson : MonoBehaviour {

    public Score myScore;
	public GameObject whiteFade;
	public string nextScene;
    public int score;
    public LeaderBoard leaderboard;
    public DrawLeaderBoard drawLeaderBoard;
    public InputField inputfield;
    public string serverName;
    public string siteAdd;
    public string siteGet;
    public bool IsOnline;

    public void BeginTestConnection()
    {
        myScore = new Score();
        myScore.name = inputfield.text;
        myScore.score = PlayerPrefs.GetInt("Score");

        Debug.Log("name -> " + myScore.name);

        StartCoroutine(TestConnection());
    }

	public IEnumerator AutomaticGoToSceneIn(float timer, string name)
	{
		
		Debug.Log("LoadScene");
		yield return new WaitForSeconds(timer);
        whiteFade.SetActive (true);
		Debug.Log("LoadScene after timer");
		yield return new WaitForSeconds (2.5f);
		SceneManager.LoadScene(name);
	}

    IEnumerator TestConnection()
    {
        WWW www = new WWW(serverName);

        yield return www;

        Debug.Log("TestingConnection...");
        if (string.IsNullOrEmpty(www.error))
        {
            IsOnline = true;
            Debug.Log("App have internet connection !");
            StartCoroutine(GetJsonFromWebService());
            // there is an internet connection
        }
        else
        {
            Debug.Log("No internet connection !");
            IsOnline = false;
            // no internet connection
        }
    }

    public IEnumerator DecodeHtmlChars(string source, Action<string> str)
    {
        string result = source.Replace("&quot;", "\"");
        result = result.Replace("\\/", "/");
        result = result.Replace("&egrave;", "è");
        result = result.Replace("&agrave;", "à");
        result = result.Replace("&hellip;", "…");
        result = result.Replace("&eacute;", "é");
        result = result.Replace("&ocirc;", "ô");
        result = result.Replace("&icirc;", "î");
        result = result.Replace("&aelig;", "œ");
        result = result.Replace("&amp;", "&");
        result = result.Replace("&ccedil;", "ç");
        result = result.Replace("&#039;", "'");
        result = result.Replace("&rsquo;", "’");
        result = result.Replace("&euml;", "ë");
        result = result.Replace("&ecirc;", "ê");
        result = result.Replace("&ucirc;", "û");
        result = result.Replace("&uuml;", "ü");
        result = result.Replace("&ugrave;", "ù");
        result = result.Replace("&ndash;", "—");
        result = result.Replace("&Eacute;", "É");
        result = result.Replace("&Egrave;", "È");
        result = result.Replace("&Ecirc;", "Ê");
        result = result.Replace("&Euml;", "Ë");
        result = result.Replace("&Ccedil;", "Ç");
        result = result.Replace("&Auml;", "Ä");
        result = result.Replace("&Agrave;", "À");
        result = result.Replace("&Acirc;", "Â");
        result = result.Replace("&acirc;", "â");
        result = result.Replace("&Ocirc;", "Ô");
        result = result.Replace("&Ouml;", "Ö");
        result = result.Replace("&Ugrave;", "Ù");
        result = result.Replace("&Uuml;", "Ü");
        result = result.Replace("&Ucirc;", "Û");
        result = result.Replace("&Icirc;", "Î");
        result = result.Replace("&Iuml;", "Ï");

        str(result);

        yield break;
    }

    IEnumerator GetJsonFromWebService()
    {
        Score tempScore = null;
        WWWForm form = new WWWForm();
        form.AddField("name", myScore.name);
        form.AddField("score", myScore.score);

        WWW wwwSetPage = new WWW(siteAdd, form);

        yield return wwwSetPage;    

        Debug.Log("BeforeSetPage");
        if(wwwSetPage.error != null)
        {
            print("error SetPage");
            yield break;
        }

        string SetLeaderBoardJson = null;
        yield return StartCoroutine(DecodeHtmlChars(wwwSetPage.text, value => SetLeaderBoardJson = value));

        tempScore = JsonUtility.FromJson<Score>(SetLeaderBoardJson);

        form.AddField("id", tempScore.id);

        WWW wwwGetPage = new WWW(siteGet, form);

        yield return wwwGetPage;

        Debug.Log("BeforeGetPage");
        if (wwwGetPage.error != null)
        {
            print("error GetPage");
            yield break;
        }
 
        string GetLeaderBoardJson = null;

        yield return StartCoroutine(DecodeHtmlChars(wwwGetPage.text, value => GetLeaderBoardJson = value));
        

        leaderboard = JsonUtility.FromJson<LeaderBoard>(GetLeaderBoardJson);

        drawLeaderBoard.InstantiateLeaderBoard(leaderboard);

		yield return StartCoroutine (AutomaticGoToSceneIn (10.0f, nextScene));
        //leaderboard.Log();

        yield break;
    }
}
