using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[AddComponentMenu("Scripts/AI/Controller")]
public class AI_Controller : MonoBehaviour {

	public List<AI_Wave> waveList;
	public float spawnInterval = 10f;



	// Use this for initialization
	void Start () {

		List<AI_Wave> l = new List<AI_Wave>(GameObject.FindObjectsOfType<AI_Wave>());

		l.Sort(
			delegate(AI_Wave p1, AI_Wave p2) {
				return p1.key.CompareTo(p2.key);
			}
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
