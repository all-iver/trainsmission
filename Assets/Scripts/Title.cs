using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

    public string nextScene;
    int count = -1;
    public GameObject[] cards;
	public GameObject skipIntro;
    public AudioSource startAudio;
	public AudioSource ambiance;

    void Start() {
        foreach (GameObject go in cards)
            go.SetActive(false);
    }

	public void EnterGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
	}

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            if (count == -1)
			{
                startAudio.gameObject.SetActive(true);
				ambiance.gameObject.SetActive(false);
			}
			skipIntro.SetActive(true);

            count ++;
            if (count < cards.Length)
                cards[count].SetActive(true);
            else if (count == cards.Length)
                EnterGame();
        }        
    }

}
