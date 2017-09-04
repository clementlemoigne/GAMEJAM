using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundAnimator : MonoBehaviour {


    public void LoadSound(string name)
    {
        SoundManager.instance.LoadSound(name, 0.7f,1.0f);
    }

    public void LoadMusic(string name)
    {
        SoundManager.instance.LoadMusic(name, 0.7f);
    }
}
