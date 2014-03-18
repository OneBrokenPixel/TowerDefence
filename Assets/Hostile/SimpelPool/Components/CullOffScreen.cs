using UnityEngine;
using System.Collections;

namespace Hostile
{
	namespace SimplePool
	{
		using Core;
		namespace Components
		{
			[AddComponentMenu("Hostile/SimplePool/Components/Cull Off Screen")]
			public class CullOffScreen :  BasePoolComponent{
				#region implemented abstract members of BasePoolComponent

				public override void OnSpawn ()
				{
				}

				public override void OnDespawn ()
				{
				}

				#endregion

				void OnBecameInvisible()
				{
					pool.Despawn(gameObject);
				}

			}
		}
	}
}
