using UnityEngine;
using System.Collections;
using System;

using Hostile.SimplePool;

[AddComponentMenu("Scripts/AI/Wave")]
public class AI_Wave : MonoBehaviour {

	public string key = "default";

	[Serializable]
	public class SubWave
	{
		public int count = 1;
		public SimplePool pool;
	}

	public SubWave[] subWaves;
}
