using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twitter : MonoBehaviour {

	public GameObject tweet;

	// Use this for initialization
	void Start()
	{
		InvokeRepeating("Tweet", 1f, 3f);
	}

	void Tweet()
	{
		StartCoroutine(OnOff());
	}


	IEnumerator OnOff()
	{
		tweet.SetActive(true);
		yield return new WaitForSeconds(1f);
		tweet.SetActive(false);
	}
}
