using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndOfAudioClip : MonoBehaviour {

    public AudioClip clip;
    public float wait;
    public bool end;
	// Use this for initialization
	void Start () {
        end = false;
    }
	
    public void LaunchCheckSounds()
    {
        StartCoroutine(CheckSounds());
    }

    public IEnumerator CheckSounds()
    {
        clip = GetComponent<AudioSource>().clip;
        wait = clip.length;

        while (wait >= 0)
        {
            wait -= Time.deltaTime;
            yield return null;
        }
        
        
        clip = null;
        GetComponent<AudioSource>().clip = null;
        wait = 0.0f;
        end = true;
        yield break;
        
    }

}
