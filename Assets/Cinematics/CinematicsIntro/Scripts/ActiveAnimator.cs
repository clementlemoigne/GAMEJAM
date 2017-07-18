using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAnimator : MonoBehaviour {

    public Animator animator ;

    public void ActivateAnimatorEvent()
    {
        animator.enabled = true;
    }

}
