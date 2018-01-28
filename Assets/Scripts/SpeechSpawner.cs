using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpeechSpawner : MonoBehaviour {

    public GameObject speechBubble;
    public Sprite[] modifierIcons;
    public Sprite[] personIcons;

    public GameObject SpawnBubble(Vector2 pos, Transform parent, int modifierIcon, int personIcon) {
        GameObject bubble = Instantiate(speechBubble);
        bubble.transform.position = pos;
        bubble.transform.SetParent(parent);
        RectTransform rt = bubble.GetComponent<RectTransform>();

        Image image = rt.Find("Panel").Find("Left Slot").GetComponent<Image>();
        image.sprite = modifierIcons[modifierIcon];
        image.preserveAspect = true;

        image = rt.Find("Panel").Find("Right Slot").GetComponent<Image>();
        image.sprite = personIcons[personIcon];
        image.preserveAspect = true;

        Vector3 scale = rt.lossyScale;
        Sequence sequence = DOTween.Sequence();
        rt.localScale = Vector3.zero;
        sequence.Append(rt.DOScale(scale, 0.75f).SetEase(Ease.OutElastic, 0.1f));
        sequence.AppendInterval(3);
        sequence.Append(rt.DOScale(new Vector3(0, 0, 0), 0.25f));
        sequence.OnComplete(() => {
            Destroy(rt.gameObject);
        });
        return rt.gameObject;
    }

}
