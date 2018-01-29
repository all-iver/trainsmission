using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccusedSpriteAssigner : MonoBehaviour {

    public Image Accused, Culprit;

	void Start ()
    {
        Accused.sprite = NPCTracker.GetSprite(NPCTracker.Accused);
        Culprit.sprite = NPCTracker.GetSprite(NPCTracker.Culprit);
	}
}
