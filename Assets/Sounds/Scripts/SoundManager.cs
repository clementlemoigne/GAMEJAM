using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public bool canUpdateSounds;
    public AudioClip[] arraySounds;                   //Drag a reference to the audio source which will play the sound effects.
    public AudioSource[] arrayAudios;
    public AudioSource music;
    public Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        for (int i = 0; i < arraySounds.Length; i++)
        {
            sounds.Add(arraySounds[i].name, arraySounds[i]);
        }
    }

    public void LoadSound(string audioName, float volume, float pitch)
    {

        for (int i = 0; i < arrayAudios.Length; i++)
        {
            if (arrayAudios[i].clip == null)
            {
                arrayAudios[i].clip = sounds[audioName];
                arrayAudios[i].volume = volume;
                arrayAudios[i].pitch = pitch;
                arrayAudios[i].Play();
                arrayAudios[i].GetComponent<CheckEndOfAudioClip>().LaunchCheckSounds();
                i = arrayAudios.Length + 1;
            }
        }

    }

    public void LoadMusic(string musicName, float volume)
    {
        music.clip = sounds[musicName];
        music.volume = volume;
        music.Play();
    }

}