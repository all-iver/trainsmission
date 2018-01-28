using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverScene : MonoBehaviour {

    public string titleScene = "title";
    GameOverState gos;
    public Image npc;
    public GameObject cell;
    int state;
    public Sprite testSprite;
    public GameObject bubble;
    public Tweener tweener;

	// Use this for initialization
	void Start () {
        gos = FindObjectOfType<GameOverState>();
        if (!gos) {
            gos = new GameObject("Game Over State").AddComponent<GameOverState>();
            gos.theAccused = testSprite;
        }
        npc.sprite = gos.theAccused;
        Destroy(gos);
        gos = null;
	}

    void TweenCell() {
        cell.gameObject.SetActive(true);
        tweener = cell.GetComponent<Image>().DOFade(0, 3).SetEase(Ease.InCirc).From();
        tweener.OnComplete(() => {
            tweener = null;
        });
    }
    
    void Update() {
        if (tweener != null)
            return;
        if (Input.anyKeyDown) {
            state ++;
            if (state == 1) {
                // check win/loss here I guess
                bubble.gameObject.SetActive(false);
                TweenCell();
            }
            if (state == 2) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(titleScene);
            }
        }
    }
	
}
