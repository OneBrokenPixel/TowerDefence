using UnityEngine;
using System.Collections;

using Hostile.SimplePool.Components;

[AddComponentMenu("Scripts/AI/March")]
public class AI_March : BasePoolComponent {

	public float speed = 1.0f;
    public int hitPoints = 1;
    private int _hitpoints;
	// Use this for initialization
	void Start () {

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
            Hit(1);
            //GameObject.Destroy(coll.gameObject);
        }
    }

    public void Hit(int damage)
    {
        _hitpoints -= damage;
        if (_hitpoints <= 0)
            Kill();

    }

	public void Kill()
	{
		pool.Despawn(gameObject);
	}

	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
