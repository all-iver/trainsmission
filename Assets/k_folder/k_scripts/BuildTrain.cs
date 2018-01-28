using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTrain : MonoBehaviour {

    public GameObject[] trains;
    public GameObject caboose;
    public GameObject engine;
    public GameObject trainsHolder;


    private GameObject currentTrain;


    private void Start ()
    {
        BuildChooChoo();
    }

    void BuildChooChoo(){

        float size = Random.Range(3,8);

        for (int i = 0; i < size; i++)
        {
            currentTrain = trains[Random.Range(0, trains.Length)];
        }

    }
}
