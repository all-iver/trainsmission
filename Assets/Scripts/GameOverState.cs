using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : MonoBehaviour {

    public Sprite theAccused;

    void Start() {
        DontDestroyOnLoad(this);
    }

}
