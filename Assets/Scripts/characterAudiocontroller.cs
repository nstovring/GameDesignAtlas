using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAudiocontroller : MonoBehaviour
{


    private AudioSource audio;
    public List<AudioClip> audioClips;  


	// Use this for initialization
	void Start () {

        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame


    public void PlayFootStepRight(float amplitude)
    {
        //walking low amplitude
        if (amplitude < 1)
        {
            audio.clip = audioClips[0];
            audio.volume = amplitude;
            audio.Play();
        }
        //running high amplitude

    }

    public void PlayFootStepLeft(float amplitude)
    {

        if (amplitude < 1)
        {
            audio.clip = audioClips[1];
            audio.volume = amplitude;
            audio.Play();
        }

    }
}
