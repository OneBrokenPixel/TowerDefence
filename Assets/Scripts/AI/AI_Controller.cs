using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[AddComponentMenu("Scripts/AI/Controller")]
public class AI_Controller : MonoBehaviour {

	public List<AI_Wave> waveList;
	public float spawnInterval = 10f;
	private int _currentWave = 0;

	private Coroutine _waveSpawnerRoutine;
	public Transform spawnTransform;
	public float spawnRadius = 5;
	private IEnumerator<WaitForSeconds> waveSpawner(int wave)
	{
		
		//Debug.Log("Starting Wave " + wave);
		for( int i = wave; i < waveList.Count; i++ )
		{
			yield return new WaitForSeconds(spawnInterval);

			AI_Wave this_wave = waveList[i];

			//Debug.Log("wave " + this_wave.key);

			foreach( AI_Wave.SubWave sub in this_wave.subWaves )
			{
				//Debug.Log(sub.count);
				Quaternion rot = spawnTransform.rotation;

				for( int s = 0; s < sub.count; s++)
				{
					Vector3 vec = spawnTransform.position + ( Random.insideUnitSphere * spawnRadius );
					vec.z = 0f;
					sub.pool.Spawn(vec,rot);
				}
			}
		}
		
		//Debug.Log( waveList.Count - wave + " Waves Completed");
	}

	// Use this for initialization
	void Start () {

	
		waveList = new List<AI_Wave>(GameObject.FindObjectsOfType<AI_Wave>());

		waveList.Sort(
			delegate(AI_Wave p1, AI_Wave p2) {
				return p1.key.CompareTo(p2.key);
			}
		);

		_currentWave = 0;
		_waveSpawnerRoutine = StartCoroutine(waveSpawner(_currentWave));
	}
}
