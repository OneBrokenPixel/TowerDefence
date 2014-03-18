using UnityEngine;
using System.Collections;

using Hostile.SimplePool.Components;

[AddComponentMenu("Scripts/AI/March")]
public class AI_March : BasePoolComponent {

	public float speed = 1.0f;

	// Use this for initialization
	void Start () {

	}

	#region implemented abstract members of BasePoolComponent

	public override void OnSpawn ()
	{
		Debug.Log("adding force");
		rigidbody2D.WakeUp();
		rigidbody2D.AddForce(transform.right * speed);
	}

	public override void OnDespawn ()
	{

	}

	#endregion
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
