using UnityEngine;
using System.Collections;

using Hostile.SimplePool.Components;
using Hostile.SimplePool;

[AddComponentMenu("Scripts/AI/March")]
public class AI_March : BasePoolComponent {

	public float speed = 1.0f;
    public int hitPoints = 1;
    private int _hitpoints;

    private Vector2 _targetVel;

    public SimplePool splatPool;
    public GameObject splatGO;

    float _scale = 1f;


	// Use this for initialization
	void Start () {
        splatPool = SimplePool.FindPoolFor(splatGO);
	}

	#region implemented abstract members of BasePoolComponent

	public override void OnSpawn ()
	{
        _hitpoints = hitPoints;
        _scale = 1f;
		//Debug.Log("adding force");
		//rigidbody2D.WakeUp();
		//rigidbody2D.AddForce(transform.right * speed);

        _targetVel = transform.right * speed;
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
	void FixedUpdate () {
        Vector2 diffVel = _targetVel - rigidbody2D.velocity;
        rigidbody2D.AddForce(diffVel);
        
	}
}
