using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PigeonGrey : MonoBehaviour {

	public Sprite standing;
	private Animator pigeonAnim;
	private SpriteRenderer spriteRend;
	public Twitter twitterGrey;
	public Twitter twitterBrown;

	//switch stuff
	public RuntimeAnimatorController playerAnimator;
	public GameObject treeCloudsCamera;


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
		//Debug.Log("Land called");
		if (transform.position.x == -195)
		{
			//Debug.Log("Landing");
			spriteRend.sprite = standing;
			pigeonAnim.enabled = false;
			twitterGrey.Love();
			twitterBrown.Love();
			StartCoroutine(EndGame());
		}
	}

	IEnumerator EndGame()
	{
		yield return new WaitForSeconds(5);

		TurnBackToPlayer();
		//UnityEngine.SceneManagement.SceneManager.LoadScene("EndGameFlappy");
	}

	private void TurnBackToPlayer()
	{
		//find body double, grab position
		GameObject bodyDouble = GameObject.Find("newPlayer(Clone)");
		Vector3 bdPos = bodyDouble.transform.position;

		//find player
		GameObject player = GameObject.Find("Player");

		//unlock constraints 
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

		//move to bodyDouble position
		player.transform.position = bdPos;

		//switch player animator back to what it should be
		Animator currentAnimator = player.GetComponent<Animator>();
		currentAnimator.runtimeAnimatorController = playerAnimator;
		player.GetComponent<Animator>().enabled = true;

		//SwitchSprites
		player.GetComponent<SpriteRenderer>().sprite = bodyDouble.GetComponent<SpriteRenderer>().sprite;

		//turn body double off
		bodyDouble.SetActive(false);

		//clean up misc issues that will likely occur
		treeCloudsCamera.SetActive(false);
	}
}
