using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpeechSpawner : MonoBehaviour {

    public SpeechBubble speechBubble;
    public Sprite[] modifierIcons;
    public Sprite[] personIcons;

    public SpeechBubble SpawnBubble(Vector2 pos, Transform parent, int modifierIcon = -1, int personIcon = -1) {
        GameObject bubble = Instantiate(speechBubble).gameObject;
        bubble.transform.position = pos;
        bubble.transform.SetParent(parent);
        RectTransform rt = bubble.GetComponent<RectTransform>();

        Image image = rt.Find("Panel").Find("Left Slot").GetComponent<Image>();
        modifierIcon = modifierIcon == -1 ? Random.Range(0, modifierIcons.Length) : modifierIcon;
        image.sprite = modifierIcons[modifierIcon];
        image.preserveAspect = true;

        image = rt.Find("Panel").Find("Right Slot").GetComponent<Image>();
        personIcon = personIcon == -1 ? Random.Range(0, personIcons.Length) : personIcon;
        image.sprite = personIcons[personIcon];
        image.preserveAspect = true;

        return bubble.GetComponent<SpeechBubble>();
    }

    public void CloseBubble(GameObject bubble) {
    }

}
