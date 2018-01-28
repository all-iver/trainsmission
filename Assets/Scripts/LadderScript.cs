using UnityEngine;

public class LadderScript : MonoBehaviour
{

    Animator animator;

    private void Update()
    {
        Debug.Log(Input.GetAxis("Vertical"));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PlayerHitsLadder");
            animator = collision.GetComponent<Animator>();

            if (Input.GetAxis("Vertical") > 0.0f)
            {
                Debug.Log("PlayerMovesUP");
                animator.SetBool("Climbing", true);
            }

            if (Input.GetAxis("Vertical") < 0.0f)
            {
                Debug.Log("PlayerMovesDOWN");
                animator.SetBool("Climbing", true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && (Input.GetAxis("Vertical") > 0.0f))
        {
            Debug.Log("PlayerStaysonLadder and moves up");
            animator = collision.GetComponent<Animator>();
            animator.SetBool("Climbing", true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PlayerExitsLadder");
            animator = collision.GetComponent<Animator>();
            animator.SetBool("Climbing", false);
        }
        
    }
}