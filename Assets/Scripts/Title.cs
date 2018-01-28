using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

    public string nextScene;
    int count = -1;
    public GameObject[] cards;
    public AudioSource startAudio;

    void Start() {
        foreach (GameObject go in cards)
            go.SetActive(false);
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            if (count == -1)
                startAudio.gameObject.SetActive(true);
            count ++;
            if (count < cards.Length)
                cards[count].SetActive(true);
            else if (count == cards.Length)
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }        
    }

}
