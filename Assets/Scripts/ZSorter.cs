using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSorter : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    public Transform bottom;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
        if (!bottom)
            bottom = transform;
	}
	
	// Update is called once per frame
	void Update () {
		spriteRenderer.sortingOrder = Mathf.RoundToInt(bottom.position.y * 100f) * -1;
	}
}
