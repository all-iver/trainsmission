using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuser : MonoBehaviour {

    NPC current;
    bool accusing;
    SpeechBubble speechBubble;
    public Transform speechOffset;

    void OnTriggerEnter2D(Collider2D other) {
        if (current)
            return;
        NPC npc = other.gameObject.GetComponent<NPC>();
        if (!npc)
            return;
        current = npc;
        current.StopAndSpeak(transform.position);
    }

    public bool IsAccusing() {
        return accusing;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (accusing)
            return;
        NPC npc = other.gameObject.GetComponent<NPC>();
        if (!npc)
            return;
        if (npc == current) {
            current.EndSpeaking();
            current = null;
        }
    }

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
        if (Input.GetButtonDown("Fire1"))
            AccuseCurrent();
    }

}
