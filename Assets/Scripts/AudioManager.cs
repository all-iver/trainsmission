using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioMixerSnapshot mainAudio;
    public AudioMixerSnapshot outsideAudio;


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("outside");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player is outside");
            outsideAudio.TransitionTo(1f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("leaving outside");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player going inside");
            mainAudio.TransitionTo(1f);
        }
    }
}
