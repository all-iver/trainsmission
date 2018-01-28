using System.Collections;
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
        MoveNext();
    }
}
