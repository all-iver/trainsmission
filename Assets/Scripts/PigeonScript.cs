using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;

public class PigeonScript : MonoBehaviour {

    private GameObject player;
    private Transform pigeon;

    //public GameObject birdBounds;
    public GameObject newPlayer;

   // Sprite playerSprite;
   // SpriteRenderer pigeonSpriteRend;

    Animator playerAnimator;
    Animator pigeonAnimator;

    public AudioMixerSnapshot FlappyAudio;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pigeon = gameObject.transform;

       // playerSprite = player.GetComponent<SpriteRenderer>().sprite;
       //pigeonSpriteRend = gameObject.GetComponent<SpriteRenderer>();

        playerAnimator = player.GetComponent<Animator>();
        pigeonAnimator = gameObject.GetComponent<Animator>();
    }


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
        //mySequence.Append(transform.DORotate(new Vector3(0, -180, 0), 1));
        //mySequence.Append(transform.DOMove(new Vector3(-30, 4, 0), 10f));
        StartCoroutine(ChangePlayer(pigeon));
    }
    
    
    IEnumerator ChangePlayer (Transform target)
    {

        Debug.Log("pigeon = "+ pigeon);
        Debug.Log("player = " + player);

        Debug.Log("pigeonANIM = " + pigeonAnimator);
        Debug.Log("playerANIM = " + playerAnimator);

        Debug.Log("outside Music");
        FlappyAudio.TransitionTo(6f);
        yield return new WaitForSeconds(8f);

        Vector3 currentPos = gameObject.transform.position;
        Debug.Log("currentPos :" + currentPos);

        GameObject bodyDouble = Instantiate(newPlayer,player.transform.position,player.transform.rotation) as GameObject;
        player.transform.position = currentPos;

        //playerSprite = pigeonSprite;
        playerAnimator.runtimeAnimatorController = pigeonAnimator.runtimeAnimatorController;


        //birdBounds.SetActive(true);
        pigeon.gameObject.SetActive(false);
    }
        
}


