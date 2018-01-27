using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrigger : MonoBehaviour {

    public bool forward = true;

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.name != "Player")
            return;
        DoubleDragonPC player = coll.gameObject.GetComponent<DoubleDragonPC>();
        player.DoCarJump(forward ? 1 : -1);
    }

}
