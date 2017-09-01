using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimator : MonoBehaviour
{

    public Animator animatorTravelling1;
    public Animator animatorTravelling2;

    public float normalizedTime1;
    public float normalizedTime2;

    public void ActiveAnimator1()
    {
        animatorTravelling1.enabled = true;
    }

    public void ActiveAnimator2()
    {
        animatorTravelling2.enabled = true;
    }

    public void PauseAnimator1()
    {
        //normalizedTime1 = animatorTravelling1.GetAnimatorTransitionInfo(0).normalizedTime;
        animatorTravelling1.speed = 0.0f;
        
    }

    public void PauseAnimator2()
    {
        //normalizedTime2 = animatorTravelling1.GetAnimatorTransitionInfo(0).normalizedTime;
        animatorTravelling2.speed = 0.0f;   
    }

    public void PlayAnimator1(string clipName)
    {
        animatorTravelling1.speed = 0.8f;
        //animatorTravelling1.Play(clipName, 0, normalizedTime1);
    }

    public void PlayAnimator2(string clipName)
    {
        animatorTravelling1.speed = 0.8f;
        //animatorTravelling2.Play(clipName, 0, normalizedTime2);
    }
}
