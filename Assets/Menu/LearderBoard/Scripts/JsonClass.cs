using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
using System;

[Serializable]
public class Score
{
    public int id;

    public int rank;

    public string name;

    public int score;
}

[Serializable]
public class LeaderBoard
{
    public List<Score> firsts = new List<Score>();

    public Score before = new Score();

    public Score after = new Score();

    public Score current = new Score();

    public void Log()
    {

        Debug.Log("-------- ---------");
        Debug.Log("LEADERBOARD");

        Debug.Log("Firsts Counts -> " + firsts.Capacity);

        foreach (Score leaderBoard in firsts)
        {
            Debug.Log(" Id -> " + leaderBoard.id + " Nom -> " + leaderBoard.name + " Score -> " + leaderBoard.score);
        }
        Debug.Log("-------- ---------");
        Debug.Log("BEFORE ");

        Debug.Log("Nom-> " + before.name + " Score-> " + before.score + " Position -> " + before.rank);
        Debug.Log("-------- ---------");
        Debug.Log("AFTER ");

        Debug.Log("Nom-> " + after.name + " Score-> " + after.score + " Position -> " + after.rank);
        Debug.Log("-------- ---------");
    }
}


