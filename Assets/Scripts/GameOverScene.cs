using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverScene : MonoBehaviour {

    public string titleScene = "title";
    // GameOverState gos;
    public Image npc, culprit;
    public GameObject yourCell, theirCell;
    int state;
    public Sprite testSprite;
    public GameObject bubble, catBubble, hahaBubble, catHahaBubble;
    public Tweener tweener;
    public GameObject winCard, loseCard, finCard, creditsCard;
    public AudioSource winSound, loseSound;

	// Use this for initialization
	void Start () {
        winCard.SetActive(false);
        loseCard.SetActive(false);
        yourCell.SetActive(false);
        theirCell.SetActive(false);
        finCard.SetActive(false);
        creditsCard.SetActive(false);
        culprit.gameObject.SetActive(false);
        bubble.gameObject.SetActive(NPCTracker.Accused != NPCTracker.ID.Cat);
        catBubble.gameObject.SetActive(NPCTracker.Accused == NPCTracker.ID.Cat);
        hahaBubble.gameObject.SetActive(false);
        catHahaBubble.gameObject.SetActive(false);
        // gos = FindObjectOfType<GameOverState>();
        // if (!gos) {
        //     gos = new GameObject("Game Over State").AddComponent<GameOverState>();
        //     gos.theAccused = testSprite;
        // }
        // npc.sprite = gos.theAccused;
        // Destroy(gos);
        // gos = null;
	}

    void TweenCell() {
        GameObject cell;
        if (NPCTracker.AccusedCorrectly())
            cell = theirCell;
        else
            cell = yourCell;
        cell.gameObject.SetActive(true);
        tweener = cell.GetComponent<Image>().DOFade(0, 1).SetEase(Ease.InCirc).From();
        tweener.OnComplete(() => {
            tweener = null;
            if (NPCTracker.AccusedCorrectly()) {
                winSound.gameObject.SetActive(true);
            } else {
                loseSound.gameObject.SetActive(true);
                hahaBubble.gameObject.SetActive(NPCTracker.Culprit != NPCTracker.ID.Cat);
                catHahaBubble.gameObject.SetActive(NPCTracker.Culprit == NPCTracker.ID.Cat);
            }
        });
        if (!NPCTracker.AccusedCorrectly()) {
            culprit.gameObject.SetActive(true);
            culprit.DOFade(0, 1).From().SetEase(Ease.InCirc);
            npc.DOFade(0, 1).SetEase(Ease.InCirc);
        }
    }
    
    void Update() {
        if (tweener != null)
            return;
        if (Input.GetButtonDown("Jump")) {
            state ++;
            if (state == 1) {
                bubble.gameObject.SetActive(false);
                catBubble.gameObject.SetActive(false);
                TweenCell();
            }
            if (state == 2) {
                if (NPCTracker.AccusedCorrectly())
                    winCard.SetActive(true);
                else
                    loseCard.SetActive(true);
            }
            if (state == 3) {
                finCard.SetActive(true);
            }
            if (state == 4) {
                creditsCard.SetActive(true);
            }
            if (state == 5) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(titleScene);
            }
        }
    }
	
}
