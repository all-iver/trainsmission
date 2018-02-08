using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverSceneFlappy : MonoBehaviour {

    public string titleScene = "title";
    // GameOverState gos;
    int state;
    public GameObject finCard, creditsCard;
    public AudioSource winSound;
    float timer;

	// Use this for initialization
	void Start () {
        finCard.SetActive(false);
        creditsCard.SetActive(false);
	}

    
    void Update() {
        timer += Time.deltaTime;
        if ((timer >= 1 && state == 0) || Input.GetButtonDown("Jump")) {
            state ++;
            if (state == 1) {
                finCard.SetActive(true);
				winSound.gameObject.SetActive(true);
			}
            if (state == 2) {
                creditsCard.SetActive(true);
            }
            if (state == 3) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(titleScene);
            }
        }
    }
	
}
