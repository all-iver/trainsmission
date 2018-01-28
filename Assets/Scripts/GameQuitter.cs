using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel")) {
            Debug.Log("Quitting");
            Application.Quit();
        }
	}
}
