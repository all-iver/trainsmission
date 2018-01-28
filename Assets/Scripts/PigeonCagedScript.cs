using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonCagedScript : MonoBehaviour {

    Animator animator;
    public GameObject pigeon;
    private bool triggered = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("touched cage");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player touched cage");
            animator.SetBool("Open", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (triggered == false)
            {
                
                if (Input.GetKeyDown("space"))
                {
                    animator.SetBool("Empty", true);
                    print("space key was pressed");
                    GameObject FlappyBird = Instantiate(pigeon, transform.position, transform.rotation) as GameObject;
                    //lock player
                    collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    collision.GetComponent<Animator>().enabled = false;

                    triggered = true;

                }
            }
        }
    }
}
