using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using Hostile.SimplePool.Components;
using Hostile.SimplePool;

using Pathfinding;

[RequireComponent(typeof(Seeker))]
[AddComponentMenu("Scripts/AI/Base")]
public class AI_Base : BasePoolComponent
{

    public int health = 1;
    public float speed = 1.0f;
    public float nextWaypointDistance = 1.0f;
    private int _currentHealth;
    private float _currentHealthScale;
    private Vector2 _targetVel = new Vector2();
    #region astar

    Seeker _seeker;
    Path _path;
    int _currentWaypoint = 0;
    private GameObject _player;

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yey, we got a path back. Did it have an error? " + p.error);
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    #endregion

    void UpdatePosition(float nStep)
    {
        Vector3 nWaypoint = _path.vectorPath[_currentWaypoint];
        if (nWaypoint == null)
            return;
        Vector3 nWaypointVec = nWaypoint - transform.position;

        if (nWaypointVec.magnitude >= nStep)
        {
            transform.position += nWaypointVec.normalized * nStep;
        }
        else
        {
            nStep -= nWaypointVec.magnitude;
            _currentWaypoint++;
            UpdatePosition(nStep);
        }
    }


    #region unity message functions

    void Start()
    {

    }

    void OnGUI()
    {
    }

    public void FixedUpdate()
    {
        if (_path != null && _currentWaypoint <= _path.vectorPath.Count-1)
        {

            //Direction to the next waypoint
            Vector3 dir = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
            dir *= speed;
            _targetVel.Set(dir.x, dir.y);


            Vector2 diffVel = _targetVel - rigidbody2D.velocity;
            rigidbody2D.AddForce(diffVel);



            Vector3 forward = new Vector3(rigidbody2D.velocity.x,rigidbody2D.velocity.y);
            transform.rotation = Quaternion.FromToRotation(Vector3.right, forward);

            //Check if we are close enough to the next waypoint
            //If we are, proceed to follow the next waypoint
            if (Vector3.Distance(transform.position, _path.vectorPath[_currentWaypoint]) < nextWaypointDistance)
            {
                _currentWaypoint++;
                return;
            }
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }

    }

    #endregion

    #region implemented abstract members of BasePoolComponent

    public override void OnSpawn()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _seeker = GetComponent<Seeker>();

        if (_seeker == null)
        {
            gameObject.AddComponent<Seeker>();
        }
        _seeker.StartPath(transform.position, _player.transform.position, OnPathComplete);
        _currentHealth = health;
        _currentHealthScale = 1f;
    }

    public override void OnDespawn()
    {
        //throw new NotImplementedException();
    }

    #endregion

    #region implemented abstract members of BasePoolComponent

    public virtual void Hit(Vector3 direction, int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Kill(direction);
            _currentHealthScale = 0;
        }
        else
        {
            _currentHealthScale = ((float)_currentHealth) / health;
        }
    }

    public virtual void Kill(Vector3 direction)
    {
        pool.Despawn(gameObject);
    }

    #endregion
}
