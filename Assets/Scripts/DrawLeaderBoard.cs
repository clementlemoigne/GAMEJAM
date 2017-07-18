using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLeaderBoard : MonoBehaviour {

    public Vector3 PositionDelta;
    public GameObject score;
    public GameObject scoreYou;
    public GetAndPushLeaderBoardJson leaderBoardJson;

    public GameObject Before;
    public GameObject After;
    public GameObject You;


    public void InstantiateLeaderBoard(LeaderBoard leaderboard)
    {
        //Debug.Log("-------- ---------");
        //.Log("LEADERBOARD");

        //Debug.Log("Firsts Counts -> " + leaderboard.firsts.Capacity);

        for (int i = 0; i < leaderboard.firsts.Capacity;i++)
        {
            //Debug.Log(" Id -> " + leaderboard.firsts[i].id + " Nom -> " + leaderboard.firsts[i].name + " Score -> " + leaderboard.firsts[i].score);
            GameObject tempScore;
            if (i == 0)
            tempScore = Instantiate(score, PositionDelta, Quaternion.identity);
            else
            tempScore = Instantiate(score, PositionDelta*(i+1), Quaternion.identity);

            tempScore.transform.SetParent(transform,false);
            tempScore.GetComponent<GetScoreChild>().Rank.text = "" + (i + 1) + "-";
            tempScore.GetComponent<GetScoreChild>().Name.text = leaderboard.firsts[i].name;
            tempScore.GetComponent<GetScoreChild>().Score.text = "" + leaderboard.firsts[i].score;
        }
        //Debug.Log("-------- ---------");
       // Debug.Log("BEFORE ");

        //Debug.Log("Nom-> " + leaderboard.before.name + " Score-> " + leaderboard.before.score);

        GameObject tempScoreBefore = Instantiate(score, PositionDelta, Quaternion.identity);
        tempScoreBefore.transform.SetParent(Before.transform, false);

        if (string.IsNullOrEmpty(leaderboard.before.name))
        {
            tempScoreBefore.GetComponent<GetScoreChild>().Rank.gameObject.SetActive(false);
            tempScoreBefore.GetComponent<GetScoreChild>().Name.text = "PERSONNE";
            tempScoreBefore.GetComponent<GetScoreChild>().Score.gameObject.SetActive(false);
        }
        else
        {
            tempScoreBefore.GetComponent<GetScoreChild>().Rank.text = "" + leaderboard.before.rank + "-";
            tempScoreBefore.GetComponent<GetScoreChild>().Name.text = leaderboard.before.name;
            tempScoreBefore.GetComponent<GetScoreChild>().Score.text = "" + leaderboard.before.score;
        }

       
        
        //Debug.Log("-------- ---------");
        //Debug.Log("AFTER ");

        //Debug.Log("Nom-> " + leaderboard.after.name + " Score-> " + leaderboard.after.score);

        GameObject tempScoreAfter = Instantiate(score, PositionDelta, Quaternion.identity);
        tempScoreAfter.transform.SetParent(After.transform,false);

        if (string.IsNullOrEmpty(leaderboard.after.name))
        {
            tempScoreAfter.GetComponent<GetScoreChild>().Rank.gameObject.SetActive(false);
            tempScoreAfter.GetComponent<GetScoreChild>().Name.text = "PERSONNE";
            tempScoreAfter.GetComponent<GetScoreChild>().Score.gameObject.SetActive(false);
        }
        else
        {
            tempScoreAfter.GetComponent<GetScoreChild>().Rank.text = "" + leaderboard.after.rank + "-";
            tempScoreAfter.GetComponent<GetScoreChild>().Name.text = leaderboard.after.name;
            tempScoreAfter.GetComponent<GetScoreChild>().Score.text = "" + leaderboard.after.score;
        }
        //Debug.Log("-------- ---------");

        //Debug.Log("-------- ---------");
        //Debug.Log("YOU ");

        //Debug.Log("Nom-> " + leaderboard.current.name + " Score-> " + leaderboard.current.score);
        GameObject tempScoreYou = Instantiate(scoreYou, PositionDelta, Quaternion.identity);
        tempScoreYou.transform.SetParent(You.transform, false);
        tempScoreYou.GetComponent<GetScoreChild>().Rank.text = "" + leaderboard.current.rank + "-";
        tempScoreYou.GetComponent<GetScoreChild>().Name.text = leaderboard.current.name;
        tempScoreYou.GetComponent<GetScoreChild>().Score.text = "" + leaderboard.current.score;

    }

}
