using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public GameObject SpawnerRoot;
    private bool Triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Triggered) return;
        Triggered = true;

        foreach (var s in SpawnerRoot.GetComponentsInChildren<Spawner>())
            s.Spawn();
    }
}
