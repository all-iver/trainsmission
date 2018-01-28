﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC : MonoBehaviour {

    Sequence sequence;
    public float speed = 10;
    public float jumpTime = 0.5f;
    public float jumpPower = 0.5f;
    public float minDelay = 1;
    public float maxDelay = 5;
    public float minDistance = 3;
    public float maxDistance = 10;
    public Collider2D homeCollider;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public Vector2 speechOffset = new Vector2(0.36f, 3.34f);
    public Vector2 catSpeechOffset = new Vector2(0.65f, 0.89f);
    int modifierIcon;
    int personIcon;
    SpeechBubble speechBubble;
    bool stopped = false;
    bool accused = false;

    bool CheckBounds(Vector2 dest) {
        return homeCollider.OverlapPoint(dest);
    }

    void MoveNext() {
        sequence = DOTween.Sequence();
        Vector2 dest;
        float attempts = 0;
        while (true) {
            dest = transform.position + (Vector3) (Random.insideUnitCircle * Random.Range(minDistance, maxDistance));
            if (CheckBounds(dest))
                break;
            attempts ++;
            if (attempts > 50)
                return;
        }
        sequence.SetDelay(Random.Range(minDelay, maxDelay));
        float duration = Vector2.Distance(transform.position, dest) / speed;
        // sequence.Append(transform.DOMove(dest, duration));
        sequence.Append(transform.DOJump(dest, jumpPower, Mathf.CeilToInt(duration / jumpTime), duration).SetEase(Ease.Linear)
                .OnStart(() => {
                    spriteRenderer.flipX = dest.x - transform.position.x > 0 ? false : true;
                }));
        sequence.OnComplete(MoveNext);
    }

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        if (!homeCollider) {
            Debug.Log("NPC can't move without a home collider");
            return;
        }
        SpeechSpawner ss = FindObjectOfType<SpeechSpawner>();
        modifierIcon = Random.Range(0, ss.modifierIcons.Length);
        personIcon = Random.Range(0, ss.personIcons.Length);
        MoveNext();
    }

    public void StopAndSpeak(Vector2 playerPos) {
        if (stopped)
            return;
        float dir = transform.position.x - playerPos.x;
        spriteRenderer.flipX = dir < 0 ? false : true;
        if (sequence != null) {
            sequence.Kill();
            sequence = null;
        }
        stopped = true;
        Vector2 off = speechOffset;
        if (spriteRenderer.sprite.name == "cat")
            off = catSpeechOffset;
        speechBubble = FindObjectOfType<SpeechSpawner>().SpawnBubble((Vector2) transform.position + off, transform,
                modifierIcon, personIcon);
    }

    public void GetAccused(Vector2 playerPos) {
        accused = true;
        if (speechBubble) {
            speechBubble.Close();
            speechBubble.gameObject.SetActive(false);
            speechBubble = null;
        }
        float dir = transform.position.x - playerPos.x;
        Vector2 off = new Vector2(dir < 0 ? -1 : 1, 0);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOJump(transform.position + (Vector3) off, 2f, 1, 0.2f));
        sequence.Append(transform.DOJump(transform.position + (Vector3) off * 1.5f, 1f, 1, 0.2f));
        float r = dir < 0 ? 20 : -20;
        transform.DORotate(new Vector3(0, 0, r), 0.2f);
    }

    public void EndSpeaking() {
        if (speechBubble) {
            speechBubble.Close();
            speechBubble = null;
        }
        stopped = false;
        MoveNext();
    }
}
