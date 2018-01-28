using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuser : MonoBehaviour {

    NPC current;
    bool accusing;
    SpeechBubble speechBubble;
    public Transform speechOffset;

    public bool IsAccusing() {
        return accusing;
    }

    void TalkToNearest() {
        NPC nearest = null;
        float dist = 5235231532;
        foreach (Collider2D coll in Physics2D.OverlapBoxAll((Vector2) transform.position + new Vector2(0, 1), new Vector2(1, 2), 0)) {
            NPC npc = coll.gameObject.GetComponent<NPC>();
            if (!npc)
                continue;
            float ndist = Vector2.Distance(transform.position, npc.transform.position);
            if (!nearest || ndist < dist) {
                nearest = npc;
                dist = ndist;
            }
        }
        if (!nearest) {
            if (current) {
                current.EndSpeaking();
                current = null;
            }
            return;
        }
        if (nearest == current)
            return;
        if (current)
            current.EndSpeaking();
        current = nearest;
        current.StopAndSpeak(transform.position);
    }

    // void OnTriggerEnter2D(Collider2D other) {
    //     if (current)
    //         return;
    //     NPC npc = other.gameObject.GetComponent<NPC>();
    //     if (!npc)
    //         return;
    //     current = npc;
    //     current.StopAndSpeak(transform.position);
    // }

    // void OnTriggerExit2D(Collider2D other) {
    //     if (accusing)
    //         return;
    //     NPC npc = other.gameObject.GetComponent<NPC>();
    //     if (!npc)
    //         return;
    //     if (npc == current) {
    //         current.EndSpeaking();
    //         current = null;
    //     }
    // }

    IEnumerator EndGame() {
        yield return new WaitForSeconds(4);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
    }

    void AccuseCurrent() {
        if (accusing || !current || current.ID == NPCTracker.ID.None)
            return;
        NPCTracker.Accused = current.ID;
        NPCTracker.AccusedCorrectly();
        current.BecomeAccused(transform.position);
        speechBubble = FindObjectOfType<SpeechSpawner>().SpawnBubble(speechOffset.position, transform, accuse: true);
        accusing = true;
        StartCoroutine(EndGame());
    }

    void Update() {
        if (accusing)
            return;
        TalkToNearest();
        if (Input.GetButtonDown("Jump"))
            AccuseCurrent();
    }

}
