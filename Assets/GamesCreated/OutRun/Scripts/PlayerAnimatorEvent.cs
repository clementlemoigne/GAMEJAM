using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvent : MonoBehaviour {

    public InputManagerOutRunPart player;
	
	public void SwapPlayerRoadLeft()
    {
        player.playerStat.CurrentRoad--;
        player.MoveCar(player.paths[player.playerStat.CurrentRoad].transform);
    }

    public void SwapPlayerRoadRight()
    {
        player.playerStat.CurrentRoad++;
        player.MoveCar(player.paths[player.playerStat.CurrentRoad].transform);
    }
}
