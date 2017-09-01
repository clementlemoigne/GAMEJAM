using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimator : MonoBehaviour {

    public Animator myAnimator; 
	public InstantiateTextWithArray instantiateText;
    public void PauseAnimator()
    {
        myAnimator.speed = 0.0f;
    }

    public void PlayAnimator()
    {
        myAnimator.speed = 1.0f;
    }

	public void SetBool()
	{
		instantiateText.CanClick = true;
	}
}
