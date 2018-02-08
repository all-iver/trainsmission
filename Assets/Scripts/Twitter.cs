using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twitter : MonoBehaviour {

	public GameObject tweet;
	public GameObject heart;
	public GameObject music;

	// Use this for initialization
	void Start()
	{
	heart.SetActive(false);
	InvokeRepeating("Tweet", 1f, 2f);
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

	public void Love()
	{
		music.SetActive(false);
		heart.SetActive(true);
	}
}
