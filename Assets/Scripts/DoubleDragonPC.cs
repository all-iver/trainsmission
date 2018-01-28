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
    public float carJumpPower = 3;
    public float carJumpSpeed = 12;
    Accuser accuser;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        accuser = GetComponent<Accuser>();
	}
	
	// Update is called once per frame
	void Update () {
        if (jumpingBetweenCars || accuser.IsAccusing())
            return;
        Vector2 direction;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        direction = direction.normalized;
        rb.velocity = Vector2.zero;
        rb.MovePosition(transform.position + (Vector3) direction * speed * Time.deltaTime);
        animator.SetBool("Grounded", true);
        animator.SetBool("Moving", direction != Vector2.zero);
        if (direction != Vector2.zero) {
            spriteRenderer.flipX = direction.x < 0;
        } if (hat) {
            hat.flipX = spriteRenderer.flipX;
            hat.sortingOrder = spriteRenderer.sortingOrder + 1;
        }
	}

    public void DoCarJump(Vector2 dest) {
        if (jumpingBetweenCars)
            return;
        jumpingBetweenCars = true;
        animator.SetBool("Grounded", false);
        Vector3 startPos = transform.position;
        float deltaX = Mathf.Abs(startPos.x - dest.x);
        Vector3 dest3d = dest;
        dest3d.z = transform.position.z;
        transform.DOJump(dest3d, carJumpPower, 1, deltaX / carJumpSpeed).SetEase(Ease.Linear)
        // transform.DOMoveY(startPos.y + carJumpDistance.y, carJumpTime / 2).SetEase(Ease.OutCirc).SetLoops(2, LoopType.Yoyo)
        .OnComplete(() => {
            jumpingBetweenCars = false;
            animator.SetBool("Grounded", true);
        });
    }

}
