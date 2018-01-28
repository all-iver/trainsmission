using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpeechSpawner : MonoBehaviour {

    public SpeechBubble speechBubble;
    public Sprite[] modifierIcons;
    public Sprite[] personIcons;
    public Sprite accuseSprite;

    public SpeechBubble SpawnBubble(Vector2 pos, Transform parent, int modifierIcon = -1, int personIcon = -1, bool accuse = false) {
        GameObject bubble = Instantiate(speechBubble).gameObject;
        bubble.transform.position = pos;
        bubble.transform.SetParent(parent);
        RectTransform rt = bubble.GetComponent<RectTransform>();

        Image image;
        if (accuse) {
            image = rt.Find("Panel").Find("Center Slot").GetComponent<Image>();
            image.sprite = accuseSprite;
            image.preserveAspect = true;
            rt.Find("Panel").Find("Left Slot").gameObject.SetActive(false);
            rt.Find("Panel").Find("Right Slot").gameObject.SetActive(false);
        } else {
            image = rt.Find("Panel").Find("Left Slot").GetComponent<Image>();
            modifierIcon = modifierIcon == -1 ? Random.Range(0, modifierIcons.Length) : modifierIcon;
            image.sprite = modifierIcons[modifierIcon];
            image.preserveAspect = true;

            image = rt.Find("Panel").Find("Right Slot").GetComponent<Image>();
            personIcon = personIcon == -1 ? Random.Range(0, personIcons.Length) : personIcon;
            image.sprite = personIcons[personIcon];
            image.preserveAspect = true;

            rt.Find("Panel").Find("Center Slot").gameObject.SetActive(false);
        }

        return bubble.GetComponent<SpeechBubble>();
    }

}
