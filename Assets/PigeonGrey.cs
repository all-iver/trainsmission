using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PigeonGrey : MonoBehaviour {

	public Sprite standing;
	private Animator pigeonAnim;
	private SpriteRenderer spriteRend;


	private void Awake()
	{
		pigeonAnim = transform.GetComponent<Animator>();
		spriteRend = transform.GetComponent<SpriteRenderer>();
	}
	public void FlyHome()
	{
		StartCoroutine(Fly());
	}

	IEnumerator Fly()
	{
		Sequence mySequence = DOTween.Sequence();
		mySequence.Append(transform.DOMove(new Vector3(-193, 103, 0), 6f));
		mySequence.Append(transform.DOMove(new Vector3(-195, 100, 0), 1f));

		yield return new WaitForSeconds(7f);
		Land();
	}


	void Land()
	{
		Debug.Log("Land called");
		if (transform.position.x == -195)
		{
			Debug.Log("Landing");
			spriteRend.sprite = standing;
			pigeonAnim.enabled = false;
		}
	}
}
