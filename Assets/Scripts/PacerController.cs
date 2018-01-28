using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacerController : MonoBehaviour {
    
    public float scrollRate;
    public Transform player;
    public float yOffset = 0.15f;
	
	void Update () {
        float xMove = Time.deltaTime * scrollRate;
        // transform.Translate(xMove, 0, 0);
        // transform.Translate(0, Time.deltaTime, 0, Space.World);
        Vector3 p = transform.position;
        p.x += xMove;
        p.y = player.position.y + yOffset;
        transform.position = p;
	}
}
