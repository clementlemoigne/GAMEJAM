using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimatorClip : MonoBehaviour {

    public Animator animator;

    public void PlayAnimator(string clipName)
    {
        animator.Play(clipName, 0, 0.0f);
    }
}
