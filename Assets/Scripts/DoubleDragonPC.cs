using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDragonPC : MonoBehaviour {

    public float speed;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer, hat;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 direction;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        direction = direction.normalized;
        rb.MovePosition(transform.position + (Vector3) direction * speed * Time.deltaTime);
        animator.SetBool("Grounded", true);
        animator.SetBool("Moving", direction != Vector2.zero);
        if (direction != Vector2.zero)
            spriteRenderer.flipX = direction.x < 0;
        if (hat)
            hat.flipX = spriteRenderer.flipX;
	}

}
