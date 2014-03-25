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
    public int speed = 1;

    void OnTriggerEnter2D(Collider2D coll)
    {

        AI_Base ai = coll.gameObject.GetComponent<AI_Base>();

        if (ai != null)
        {
            ai.Hit(this.transform.position - coll.transform.position, damage);
        }

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
