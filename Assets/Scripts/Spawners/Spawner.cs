using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Prefab;

    [Range(0.0f, 1.0f)]
    public float Chance = 1.0f;

    public void Spawn()
    {
        if (Random.Range(0.0f, 1.0f) < Chance)
        {
            var spawnedObject = GameObject.Instantiate(Prefab);
            spawnedObject.transform.position = transform.position;
        }
    }
}
