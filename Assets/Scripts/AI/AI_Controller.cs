using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[AddComponentMenu("Scripts/AI/Controller")]
public class AI_Controller : MonoBehaviour {

	public List<AI_Wave> waveList;
	//public float spawnInterval = 10f;
	private int _currentWave = 0;

	private Coroutine _waveSpawnerRoutine;
    private AstarPath _aStar;
	//public Transform spawnTransform;
	//public float spawnRadius = 5;
	private IEnumerator<WaitForSeconds> waveSpawner(int wave)
	{
		
		//Debug.Log("Starting Wave " + wave);
		for( int i = wave; i < waveList.Count; i++ )
        {
            AI_Wave this_wave = waveList[i];
            yield return new WaitForSeconds(this_wave.spawnInterval);


			//Debug.Log("wave " + this_wave.key);

			foreach( AI_Wave.SubWave sub in this_wave.subWaves )
			{
				//Debug.Log(sub.count);
                Quaternion rot = sub.spawnPoint.transform.rotation;

				for( int s = 0; s < sub.count; s++)
				{
					sub.pool.Spawn(sub.spawnPoint.spawnCoordinate,rot);
				}
			}
		}
		
		//Debug.Log( waveList.Count - wave + " Waves Completed");
	}

	// Use this for initialization
	void Start () {

        _aStar = FindObjectOfType<AstarPath>();

        _aStar.AutoScan();

		waveList = new List<AI_Wave>(GameObject.FindObjectsOfType<AI_Wave>());

		waveList.Sort(
			delegate(AI_Wave p1, AI_Wave p2) {
				return p1.name.CompareTo(p2.name);
			}
		);

		_currentWave = 0;
		_waveSpawnerRoutine = StartCoroutine(waveSpawner(_currentWave));
	}
}
