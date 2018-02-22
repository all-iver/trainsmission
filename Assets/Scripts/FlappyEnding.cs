using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FlappyEnding : MonoBehaviour {

	public GameObject treeClouds;
	public GameObject treeCloudsCamera;
	public GameObject whitePanel;
	public GameObject cloudRight;
	public GameObject cloudLeft;

	public PigeonGrey pigeonGrey;

	private GameObject player;


	private void Awake()
	{
		treeClouds.SetActive(false);
		treeCloudsCamera.SetActive(false);
	}


	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			player = collision.gameObject;
			//Debug.Log("TriggerFlappyEnding");
			StartCoroutine(Ending());
		}
	}

	IEnumerator Ending()
	{
		StartCoroutine(FadeSR(1.0f, 2f, cloudLeft));
		StartCoroutine(FadeSR(1.0f, 2f, cloudRight));
		StartCoroutine(FadeImg(1.0f, 3f, whitePanel));
		yield return new WaitForSeconds(3f);
		StartCoroutine(Ending2());
	}

	IEnumerator FadeSR(float aValue, float aTime, GameObject gameObject)
	{
		float alpha = gameObject.GetComponent<SpriteRenderer>().color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
			gameObject.GetComponent<SpriteRenderer>().color = newColor;
			yield return null;
		}
	}


	IEnumerator FadeImg(float aValue, float aTime, GameObject gameObject)
	{
		float alpha = gameObject.GetComponent<Image>().color.a;

		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
			gameObject.GetComponent<Image>().color = newColor;
			yield return null;
		}
	}


	IEnumerator Ending2()
	{
		//lock player
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		//collision.GetComponent<Animator>().enabled = false;

		//turn on TreeClouds + Camera
		treeClouds.SetActive(true);
		treeCloudsCamera.SetActive(true);

		//move player
		//Vector3 currentPos = player.transform.position;
		//Debug.Log("currentPos :" + currentPos);
		player.transform.position = new Vector3(-200, 100, 0);
		//Vector3 newPos = player.transform.position;
		//Debug.Log("newPos :" + newPos);

		yield return new WaitForSeconds(1f);
		//Debug.Log("moving clouds");

		StartCoroutine(FadeSR(0.0f, 5f, cloudLeft));
		StartCoroutine(FadeSR(0.0f, 5f, cloudRight));
		StartCoroutine(FadeImg(0.0f, 3f, whitePanel));

		//move Cloud Cover
		cloudRight.transform.DOMoveX(-60, 6);
		cloudLeft.transform.DOMoveX(-240, 6);

		yield return new WaitForSeconds(3f);

		pigeonGrey.FlyHome();
	}

		//GameObject bodyDouble = Instantiate(newPlayer, player.transform.position, player.transform.rotation) as GameObject;
		//unlock player
		//player.GetComponent<Animator>().enabled = true;
		//player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
		//player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

		//player.transform.position = currentPos;

		//playerSprite = pigeonSprite;
		//playerAnimator.runtimeAnimatorController = pigeonAnimator.runtimeAnimatorController;


		//birdBounds.SetActive(true);
		//pigeon.gameObject.SetActive(false);

	private void CloudCover()
	{
		throw new NotImplementedException();
	}
}
