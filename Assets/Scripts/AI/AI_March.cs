using UnityEngine;
using System.Collections;

using Hostile.SimplePool.Components;
using Hostile.SimplePool;

[AddComponentMenu("Scripts/AI/March")]
public class AI_March : BasePoolComponent {

	public float speed = 1.0f;
    public int hitPoints = 1;
    private int _hitpoints;

    public SimplePool splatPool;
    public GameObject splatGO;

	// Use this for initialization
	void Start () {
        splatPool = SimplePool.FindPoolFor(splatGO);
	}

	#region implemented abstract members of BasePoolComponent

	public override void OnSpawn ()
	{
        _hitpoints = hitPoints;
		//Debug.Log("adding force");
		rigidbody2D.WakeUp();
		rigidbody2D.AddForce(transform.right * speed);
	}

	public override void OnDespawn ()
	{

	}

	#endregion

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Weaponry")
        {

            Vector3 dir = coll.gameObject.transform.position - transform.position;

            Hit(1, dir);
        }
    }

    public void Hit(int damage,Vector3 direction)
    {
        _hitpoints -= damage;
        if (_hitpoints <= 0)
            Kill(direction);

    }

	public void Kill(Vector3 direction)
	{
        if( splatPool != null )
        {
            Debug.Log(direction);
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
	
	}
}
