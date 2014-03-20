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

    private Vector3 _rand3d = new Vector3();
    private Vector2 _rand2d = new Vector2();

    public Vector3 spawnCoordinate
    {
        get
        {
            _rand2d = UnityEngine.Random.insideUnitCircle;

            _rand3d.Set(_rand2d.x, _rand2d.y, 0.0f);
            return transform.position + (_rand3d * _collider.radius);
        }
    }
}

