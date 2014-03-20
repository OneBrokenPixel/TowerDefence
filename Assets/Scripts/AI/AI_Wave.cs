using UnityEngine;
using System.Collections;
using System;

using Hostile.SimplePool;

[AddComponentMenu("Scripts/AI/Wave")]
public class AI_Wave : MonoBehaviour
{

    public float spawnInterval = 10f;

	[Serializable]
	public class SubWave
	{
        public AI_SubWaveSpawn spawnPoint;
		public int count = 1;
		public SimplePool pool;
	}

	public SubWave[] subWaves;
}
