using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuser : MonoBehaviour {

    NPC current;
    bool accusing;
    AccusationBubble speechBubble;
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
	
    IEnumerator EndGame() {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndGame");
    }

	bool AccusationAborted = false;
	float AccusationWindup = 0.0f;
	NPCTracker.ID AccusedNPC = NPCTracker.ID.None;

    void AccuseCurrent() {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Grounded", true);
        animator.SetBool("Moving", false);

		speechBubble.OnAccusationMade();

        NPCTracker.Accused = current.ID;
        NPCTracker.AccusedCorrectly();
        current.BecomeAccused(transform.position);
        accusing = true;
		
        StartCoroutine(EndGame());
    }

	void UpdateAccusationWindup()
	{
		bool buttonHeld = Input.GetButton("Jump");
		bool buttonPressed = Input.GetButtonDown("Jump");

		if (buttonHeld && !AccusationAborted)
		{
			if (buttonPressed && current)
			{
				AccusedNPC = current.ID;
				speechBubble = FindObjectOfType<SpeechSpawner>().SpawnAccusationBubble(speechOffset.position, transform);
			}

			if (!current || current.ID != AccusedNPC)
			{
				AccusationAborted = true;
			}
			else if (AccusationWindup < 1.0f)
			{
				AccusationWindup = Mathf.Clamp01(AccusationWindup + Time.deltaTime / 1.5f);
				speechBubble.ShowAccusationWindup(AccusationWindup);
			}
			else
			{
				AccuseCurrent();
			}
		}
		else
		{
			if (!buttonHeld)
			{
				AccusationAborted = false;
			}

			if (speechBubble != null)
			{
				speechBubble.Close();
				speechBubble = null;
			}

			AccusationWindup = 0.0f;
			AccusedNPC = NPCTracker.ID.None;
		}
	}

    void Update() {
        if (accusing)
            return;
        TalkToNearest();
		UpdateAccusationWindup();
    }

}
