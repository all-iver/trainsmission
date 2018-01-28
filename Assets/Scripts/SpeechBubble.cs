using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour {

    Sequence sequence;

	// Use this for initialization
	void Start () {
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 scale = rt.lossyScale;
        sequence = DOTween.Sequence();
        rt.localScale = Vector3.zero;
        sequence.Append(rt.DOScale(scale, 0.75f).SetEase(Ease.OutElastic, 0.1f));
	}

    public void Close() {
        RectTransform rt = GetComponent<RectTransform>();
        sequence.Complete();
        sequence.Append(rt.DOScale(new Vector3(0, 0, 0), 0.25f));
        sequence.OnComplete(() => {
            Destroy(rt.gameObject);
        });
    }

}
