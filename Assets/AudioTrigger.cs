using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource soundTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered)");
        if (collision.CompareTag("Player"))
        {
            soundTrigger.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exit)");
        if (collision.CompareTag("Player"))
        {
            soundTrigger.Stop();
        }
                  
    }
}