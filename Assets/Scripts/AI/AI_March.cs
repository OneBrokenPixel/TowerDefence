using UnityEngine;
using System.Collections;

using Hostile.SimplePool.Components;
using Hostile.SimplePool;

using Pathfinding;

[AddComponentMenu("Scripts/AI/March")]
public class AI_March : BasePoolComponent {

	public float speed = 1.0f;
    public int hitPoints = 1;
    private int _hitpoints;

    private Vector2 _targetVel;

    public SimplePool splatPool;
    public GameObject splatGO;

    private GameObject _player;

    Seeker _seeker;
    Path _path;
    int _currentWaypoint = 0;
    public float nextWaypointDistance = 3;

    float _scale = 1f;


	// Use this for initialization
	void Start () {
        splatPool = SimplePool.FindPoolFor(splatGO);
	}

	#region implemented abstract members of BasePoolComponent

	public override void OnSpawn ()
	{

        _player = GameObject.FindGameObjectWithTag("Player");
        _seeker = GetComponent<Seeker>();
        _seeker.StartPath(transform.position, _player.transform.position, OnPathComplete);
        _hitpoints = hitPoints;
        _scale = 1f;

        _targetVel = transform.right * speed;
	}

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yey, we got a path back. Did it have an error? " + p.error);
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

	public override void OnDespawn ()
	{

	}

	#endregion
    /*
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Weaponry")
        {

            Vector3 dir = coll.gameObject.transform.position - transform.position;

            Hit(1, dir);
        }
    }
    */
    private static Color orange = new Color(1, 1, 0);
    void OnGUI()
    {
        
        Vector2 screen = Camera.main.WorldToScreenPoint(transform.position);

        if (_scale > 0.75f)
            GUI.backgroundColor = Color.green;
        else if (_scale > 0.5f)
            GUI.backgroundColor = Color.yellow;
        else if (_scale > 0.25f)
            GUI.backgroundColor = orange;
        else
            GUI.backgroundColor = Color.red;

        GUI.Button(new Rect(screen.x - (8 * transform.localScale.x), (Screen.height - screen.y) + (8 * transform.localScale.y), (16 * transform.localScale.x) * _scale, 4), "");
    }

    public void Hit(int damage,Vector3 direction)
    {
        _hitpoints -= damage;
        if (_hitpoints <= 0)
        {
            Kill(direction);
            _scale = 0;
        }
        else
        {
            _scale = ((float)_hitpoints) / hitPoints;
        }

    }

	public void Kill(Vector3 direction)
	{
        if( splatPool != null )
        {
            //Debug.Log(direction);
            GameObject splat = splatPool.Spawn();
            splat.transform.position = transform.position + (Vector3.forward*5);
            splat.transform.rotation = Quaternion.FromToRotation(-Vector3.right, direction);
            splat.transform.localScale = transform.localScale;
        }
        Remove();
	}

    public void Remove()
    {
        pool.Despawn(gameObject);
    }

	// Update is called once per frame
    public void FixedUpdate()
    {
        if (_path == null)
        {
            //We have no path to move after yet
            return;
        }

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            Debug.Log("End Of Path Reached");
            return;
        }

        //Direction to the next waypoint
        Vector3 dir = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
        dir *= speed;
        _targetVel.Set(dir.x, dir.y);

            
        Vector2 diffVel = _targetVel - rigidbody2D.velocity;
        rigidbody2D.AddForce(diffVel);

        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, _path.vectorPath[_currentWaypoint]) < nextWaypointDistance)
        {
            _currentWaypoint++;
            return;
        }
    }
}
