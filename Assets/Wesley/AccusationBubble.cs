using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AccusationBubble : MonoBehaviour
{
#pragma warning disable 649
	[SerializeField] Image BackgroundIcon;
	[SerializeField] Image ForegroundIcon;
#pragma warning restore 649

	Sequence sequence;
	RectTransform RectTransform;

	void TweenIn()
	{
		if (sequence != null)
			sequence.Complete();

		RectTransform rt = RectTransform;
		Vector3 scale = rt.lossyScale;
		sequence = DOTween.Sequence();
		rt.localScale = Vector3.zero;
		sequence.Append(rt.DOScale(scale, 0.75f).SetEase(Ease.OutElastic, 0.1f));
	}

	void TweenOut()
	{
		if (sequence != null)
			sequence.Complete();

		sequence = DOTween.Sequence();
		sequence.Append(RectTransform.DOScale(new Vector3(0, 0, 0), 0.25f));
		sequence.OnComplete(() => {
			Destroy(RectTransform.gameObject);
		});
	}

	public void ShowAccusationWindup(float lerp)
	{
		BackgroundIcon.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
		ForegroundIcon.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
		ForegroundIcon.fillAmount = lerp;

	}

	public void OnAccusationMade()
	{
		BackgroundIcon.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		ForegroundIcon.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		TweenIn();
	}

	void Start()
	{
		RectTransform = GetComponent<RectTransform>();
		TweenIn();
	}

	public void Close()
	{
		TweenOut();
	}
}
