using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/AI/Kill Trigger")]
public class AI_KillTrigger : MonoBehaviour {

	void OnTriggerExit2D( Collider2D coll )
	{
		//Debug.Log("Killing " + coll.gameObject.name);
		AI_March march =  coll.gameObject.GetComponent<AI_March>();

		if( march != null )
		{
			march.Kill ();
		}
	}
}
