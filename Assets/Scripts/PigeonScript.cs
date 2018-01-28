using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PigeonScript : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        // Grab a free Sequence to use
        Sequence mySequence = DOTween.Sequence();
        // Add a movement tween at the beginning
        mySequence.Append(transform.DOMove(new Vector3(-18, 2, 0), 5f));
       // mySequence.Append(transform.DOMoveX(-20, 4f));
        // Add a rotation tween as soon as the previous one is finished
        mySequence.Append(transform.DORotate(new Vector3(0, 180, 0), 1));
        // Delay the whole Sequence by 1 second
        mySequence.PrependInterval(1);
        mySequence.Append(transform.DORotate(new Vector3(0, -180, 0), 1));
        mySequence.Append(transform.DOMove(new Vector3(-30, 4, 0), 10f));

    }
}


