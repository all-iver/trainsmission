using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlappyAudioSwitch : MonoBehaviour
{

    public AudioMixerSnapshot FlappyAudio;
    public AudioSource FlappySource;

    public void Flappy()
    {
        FlappySource.Play();
        FlappyAudio.TransitionTo(6f);
    }
}