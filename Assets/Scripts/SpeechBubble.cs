using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
#pragma warning disable 649
	[SerializeField] SpeechIcon Icon_Left;
	[SerializeField] SpeechIcon Icon_Right;
	[SerializeField] SpeechIcon Icon_Big;
#pragma warning restore 649

	Sequence sequence;

	public void Say(Sprite icon1, Sprite icon2, SpeechEmotion emotion = SpeechEmotion.Loopy)
	{
		Icon_Big.gameObject.SetActive(false);
		Icon_Left.gameObject.SetActive(true);
		Icon_Right.gameObject.SetActive(true);

		Icon_Left.SetIcon(icon1);
		Icon_Right.SetIcon(icon2);

		Icon_Left.SetEmotion(emotion);
		Icon_Right.SetEmotion(emotion);
	}

	public void Accuse()
	{
		Icon_Big.gameObject.SetActive(true);
		Icon_Left.gameObject.SetActive(false);
		Icon_Right.gameObject.SetActive(false);
	}

	public void DrunkenAccuse()
	{
		Icon_Big.gameObject.SetActive(true);
		Icon_Left.gameObject.SetActive(true);
		Icon_Right.gameObject.SetActive(true);

		Icon_Big.SetEmotion(SpeechEmotion.Loopy);
		Icon_Left.SetEmotion(SpeechEmotion.Drunk);
		Icon_Right.SetEmotion(SpeechEmotion.Drunk);

		Icon_Big.transform.parent.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

		Icon_Left.SetIcon(SpeechHelper.GetIcon_Certainty(false));
		Icon_Right.SetIcon(SpeechHelper.DrunkSprite);
	}

	void Start()
	{
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 scale = rt.lossyScale;
        sequence = DOTween.Sequence();
        rt.localScale = Vector3.zero;
        sequence.Append(rt.DOScale(scale, 0.75f).SetEase(Ease.OutElastic, 0.1f));
	}

    public void Close()
	{
        RectTransform rt = GetComponent<RectTransform>();
        sequence.Complete();
        sequence.Append(rt.DOScale(new Vector3(0, 0, 0), 0.25f));
        sequence.OnComplete(() => {
            Destroy(rt.gameObject);
        });
    }

}
