using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpeechSpawner : MonoBehaviour
{
	public SpeechBubble speechBubble;
    public Sprite accuseSprite;

	public SpeechBubble SpawnBubble(Vector2 pos, Transform parent)
	{
		GameObject bubble = Instantiate(speechBubble).gameObject;
		bubble.transform.position = pos;
		bubble.transform.SetParent(parent);
		return bubble.GetComponent<SpeechBubble>();
	}
}
