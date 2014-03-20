using UnityEngine;
using System.Collections;

public class AI_Counter : MonoBehaviour {

    private int _count = 0;

	// Use this for initialization
	void Start () {
        _count = 0;
	}

    void OnTriggerExit2D(Collider2D coll)
    {
        //Debug.Log("Killing " + coll.gameObject.name);
        AI_March march = coll.gameObject.GetComponent<AI_March>();

        if (march != null)
        {
            _count += 1;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 22), ": " + _count);
    }
}
