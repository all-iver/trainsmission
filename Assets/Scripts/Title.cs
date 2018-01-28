using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

    public string nextScene;
    int count = -1;
    public GameObject[] cards;

    void Start() {
        foreach (GameObject go in cards)
            go.SetActive(false);
    }

    void Update() {
        if (Input.anyKeyDown) {
            count ++;
            if (count < cards.Length)
                cards[count].SetActive(true);
            else if (count == cards.Length)
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
        }        
    }

}
