using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuser : MonoBehaviour {

    NPC current;

    void OnTriggerEnter2D(Collider2D other) {
        if (current)
            return;
        NPC npc = other.gameObject.GetComponent<NPC>();
        if (!npc)
            return;
        current = npc;
        current.StopAndSpeak();
    }

    void OnTriggerExit2D(Collider2D other) {
        NPC npc = other.gameObject.GetComponent<NPC>();
        if (!npc)
            return;
        if (npc == current) {
            Debug.Log("Bye " + current.name);
            current.EndSpeaking();
            current = null;
        }
    }

}
