using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour {

    public bool forward = true;
    public float carJumpTime = 0.5f;
    public Vector2 carJumpDistance = new Vector2(6, 3);

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.name != "Player")
            return;
        DoubleDragonPC player = coll.gameObject.GetComponent<DoubleDragonPC>();
        player.DoCarJump(forward ? 1 : -1, carJumpTime, carJumpDistance);
    }

}
