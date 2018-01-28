﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverScene : MonoBehaviour {

    public string titleScene = "title";
    // GameOverState gos;
    public Image npc;
    public GameObject yourCell, theirCell;
    int state;
    public Sprite testSprite;
    public GameObject bubble;
    public Tweener tweener;
    public GameObject winCard, loseCard, finCard;

	// Use this for initialization
	void Start () {
        winCard.SetActive(false);
        loseCard.SetActive(false);
        yourCell.SetActive(false);
        theirCell.SetActive(false);
        finCard.SetActive(false);
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
                bubble.gameObject.SetActive(false);
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
                UnityEngine.SceneManagement.SceneManager.LoadScene(titleScene);
            }
        }
    }
	
}
