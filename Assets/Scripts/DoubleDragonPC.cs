using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoubleDragonPC : MonoBehaviour {

    public float speed;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public SpriteRenderer hat;
    bool jumpingBetweenCars;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (jumpingBetweenCars)
            return;
        Vector2 direction;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        direction = direction.normalized;
        rb.MovePosition(transform.position + (Vector3) direction * speed * Time.deltaTime);
        // rb.velocity = direction * speed * Time.deltaTime;
        animator.SetBool("Grounded", true);
        animator.SetBool("Moving", direction != Vector2.zero);
        if (direction != Vector2.zero) {
            spriteRenderer.flipX = direction.x < 0;
        } if (hat) {
            hat.flipX = spriteRenderer.flipX;
        }
	}

    public void DoCarJump(int direction, float carJumpTime, Vector2 carJumpDistance) {
        if (jumpingBetweenCars)
            return;
        jumpingBetweenCars = true;
        animator.SetBool("Grounded", false);
        Debug.Log("Start car jump");
        Vector3 startPos = transform.position;
        transform.DOMoveX(startPos.x + carJumpDistance.x * direction, carJumpTime).SetEase(Ease.Linear);
        transform.DOMoveY(startPos.y + carJumpDistance.y, carJumpTime / 2).SetEase(Ease.OutCirc).SetLoops(2, LoopType.Yoyo)
        .OnComplete(() => {
            jumpingBetweenCars = false;
            animator.SetBool("Grounded", true);
        });
    }

}
