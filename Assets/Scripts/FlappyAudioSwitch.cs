using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FlappyAudioSwitch : MonoBehaviour
{

    public AudioMixerSnapshot FlappyAudio;
    public AudioSource FlappySource;
    public GameObject resetButton;

    public void Flappy()
    {
        resetButton.SetActive(true);
        FlappySource.Play();
        FlappyAudio.TransitionTo(6f);
    }
}