using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccusedSpriteAssigner : MonoBehaviour {

    public Image Image;

	void Start ()
    {
        Image.sprite = NPCTracker.GetSprite(NPCTracker.Accused);
	}
}
