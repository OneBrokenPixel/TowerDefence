using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(CircleCollider2D))]
[AddComponentMenu("Scripts/AI/Wave Spawn")]
public class AI_SubWaveSpawn : MonoBehaviour
{
    CircleCollider2D _collider;

    // Use this for initialization
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;
    }

    public Vector3 spawnCoordinate
    {
        get
        {
            Vector3 rand = UnityEngine.Random.insideUnitSphere;
            rand.z = 0;
            rand.Normalize();
            return transform.position + (rand*_collider.radius);
        }
    }
}

