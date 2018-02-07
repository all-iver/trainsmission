using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyEnding : MonoBehaviour {

	public GameObject coverCloud;

	private GameObject player;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			player = collision.gameObject;
			Debug.Log("TriggerFlappyEnding");
			StartCoroutine(FadeTo(1.0f, 2f));
			
		}
	}

	IEnumerator FadeTo(float aValue, float aTime)
	{

		float alpha = coverCloud.GetComponent<SpriteRenderer>().color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
			coverCloud.GetComponent<SpriteRenderer>().color = newColor;
			yield return null;
		}
		//StartCoroutine(Ending());
	}


	IEnumerator Ending()
	{
		//lock player
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		//collision.GetComponent<Animator>().enabled = false;

		//CloudCover();
		yield return new WaitForSeconds(2f);

		Vector3 currentPos = player.transform.position;
		Debug.Log("currentPos :" + currentPos);

		player.transform.position = new Vector3(-200, 100, 0);

		Vector3 newPos = player.transform.position;
		Debug.Log("newPos :" + newPos);
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
	}

	private void CloudCover()
	{
		throw new NotImplementedException();
	}
}
