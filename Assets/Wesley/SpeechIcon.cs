using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpeechIcon : MonoBehaviour
{
#pragma warning disable 649
	[SerializeField] private Animator Animator;
#pragma warning restore 649

	public Image Image;

	public void SetIcon(Sprite icon)
	{
		Image.sprite = icon;
	}

	public void SetEmotion(SpeechEmotion emotion)
	{
		Animator.SetInteger("Emotion", (int)emotion);
		Animator.SetTrigger("EmotionChanged");
	}
}
