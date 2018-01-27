using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacerController : MonoBehaviour {
    
    public float scrollRate;
	
	void Update () {
       float xMove = Time.deltaTime * scrollRate;
        transform.Translate(xMove, 0, 0);
       // transform.Translate(0, Time.deltaTime, 0, Space.World);
	}
}
