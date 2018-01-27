﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrollyman : MonoBehaviour {

    public GameObject prefab;
    List<GameObject> instances = new List<GameObject>();
    [Range(0, 1)]
    public float threshold = 0.8f;
    float distanceTillNext;
    public float width = 30;
    [Range(0.1f, 100)]
    public float step = 0.5f;
    float almostEquals = 0.05f;
    Grid grid;
    Transform children;
    Camera cam;
    float seed;

	// Use this for initialization
	void Start () {
        children = new GameObject("children").transform;
        children.SetParent(transform);
        grid = children.gameObject.AddComponent<Grid>();
        grid.cellSize = new Vector2(step, step);
        cam = Camera.main;
        seed = Random.Range(10f, 999f);
	}

    void SpawnInstance(float x) {
        GameObject go = Instantiate(prefab, new Vector3(x, transform.position.y, transform.position.z), 
                Quaternion.identity);
        go.transform.SetParent(children);
        instances.Add(go);
    }

    bool InstanceExists(float x) {
        foreach (GameObject go in instances)
            if (Mathf.Abs(go.transform.position.x - x) < almostEquals)
                return true;
        return false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 p = cam.transform.position;

        // remove instances outside our bounds
        for (int i = 0; i < instances.Count; i++) {
            GameObject go = instances[i];
            if (go.transform.position.x < p.x - width / 2 || go.transform.position.x > p.x + width / 2) {
                Destroy(go);
                instances.Remove(go);
                i --;
            }
        }

        // spawn new instances at each grid position if they don't already exist
        float left = p.x - width / 2;
        float right = p.x + width / 2;
        float nx = left;
        while (nx < right) {
            Vector3 pos = grid.CellToWorld(grid.WorldToCell(new Vector3(nx, p.y, p.z)));
            float v = Mathf.PerlinNoise(pos.x * seed, pos.y * seed);
            if (v > threshold)
                if (!InstanceExists(pos.x))
                    SpawnInstance(pos.x);
            nx += step;
        }
	}

    void OnValidate() {
        if (!Application.isPlaying)
            return;
        for (int i = 0; i < instances.Count; i++)
            Destroy(instances[i]);
        instances.Clear();
    }
}