
using UnityEngine;
using System.Collections;

namespace Hostile
{
	namespace SimplePool
	{
		using Core;
		using Components;
		[AddComponentMenu("Hostile/SimplePool/Pool")]
		public class SimplePool : MonoBehaviour
		{

			public GameObject prefab;
			public int maxCount = 0;
			private int _count = 0;
			public int batchCreateCount = 5;
			public bool cullInactive = false;
			public float cullInterval = 10f;

			private bool isFull {
				get { return ( _count < maxCount || maxCount == 0  ); }
			}

			ComponentList _inactive = null;
			ComponentList _active = null;


			IEnumerator CullRoutine()
			{
				while (cullInactive)
				{
					yield return new WaitForSeconds(cullInterval);

					//Debug.Log("Culling");

					int cullCount = _inactive.Count - batchCreateCount;
					if (cullCount >0)
					{
						RemoveGameObjects(_inactive, cullCount);
					}
				}
			}

			private void AddNewGameObjects()
			{
				if( prefab != null )
				{
					//Debug.Log ("Adding New Game Objects to " + prefab.name + " pool");
					for (int i = 0; i < batchCreateCount && isFull; i++)
					{
						//Debug.Log(i);
						GameObject obj = GameObject.Instantiate(prefab) as GameObject;

						obj.SetActive(false);
						obj.transform.parent = transform;

						BasePoolComponent bhv = obj.GetComponent<BasePoolComponent>();

						if(bhv != null)
							bhv.OnCreatedByPool(this);

						_inactive.InsertAtTail(ref obj);
						_count++;
					}
				}
			}

			void RemoveGameObjects(ComponentList list, int count)
			{
				for (int i = 0; i< count && !list.isEmpty; i++)
				{
					GameObject obj = list.RemoveTailGO();
					GameObject.Destroy(obj);
					_count--;
				}
			}

			public GameObject Spawn()
			{
				return Spawn (transform.position, transform.rotation);
			}

			public GameObject Spawn( Vector3 position, Quaternion rotation )
			{
				if(prefab == null)
				{
					return null;
				}

				if( _inactive.isEmpty && isFull)
				{
					AddNewGameObjects();
				}

				if(!_inactive.isEmpty)
				{
					GameObject obj = _inactive.RemoveHeadGO();
					if( obj != null )
					{

						obj.transform.position = position;
						obj.transform.rotation = rotation;

						if( rigidbody != null )
						{
							rigidbody.velocity = Vector3.zero;
							rigidbody.angularVelocity = Vector3.zero;
						}

						if( rigidbody2D != null )
						{
							rigidbody2D.velocity = Vector2.zero;
							rigidbody2D.angularVelocity = 0f;
						}

						
						obj.SetActive(true);

						BasePoolComponent cmp = obj.GetComponent<BasePoolComponent>();
						if( cmp != null )
						{
							cmp.OnSpawn();
						}


						_active.InsertAtTail(ref obj);

						return obj;
					}
				}

				return null;
			}

			public void Despawn(GameObject obj)
			{
				PoolListableComponent listable = obj.GetComponent<PoolListableComponent>();
				if( listable != null )
				{
					if( listable.list.Equals(_active) )
					{
						obj.SetActive(false);

						BasePoolComponent cmp = obj.GetComponent<BasePoolComponent>();

						if( cmp != null)
						{
							cmp.OnDespawn();
						}

						_active.Remove(obj);
						_inactive.InsertAtTail(ref obj);
					}
				}
			}

			// Use this for initialization
			void Start ()
			{
			
				if( prefab != null )
				{
					PoolListableComponent listable = prefab.GetComponent<PoolListableComponent>();
					if( listable == null )
					{
						prefab.AddComponent<PoolListableComponent>();
					}
				}

				_inactive = new ComponentList ();
				_active = new ComponentList ();
				StartCoroutine (CullRoutine ());
				/*
				for(int i = 0; i < 6; i++)
					Spawn();
					*/
			}
		}
	}
}
