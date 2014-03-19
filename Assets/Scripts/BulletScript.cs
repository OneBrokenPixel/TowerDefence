using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    /*
    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
    */

    public int damage = 1;

    void OnTriggerEnter2D(Collider2D coll)
    {

        AI_March ai = coll.gameObject.GetComponent<AI_March>();

        if (ai != null)
        {
            ai.Hit(damage, this.transform.position - coll.transform.position );
        }

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
