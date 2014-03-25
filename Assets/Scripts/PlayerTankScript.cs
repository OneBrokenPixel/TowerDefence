using UnityEngine;
using System.Collections;

public class PlayerTankScript : MonoBehaviour {

    public GameObject gun;
    public GameObject body;
    public GameObject bullet;

    private float fireRate = 0.3f;
    private float nextFire = 0.0f;

	// Use this for initialization
	void Start () {
        //Screen.showCursor = false; 
	}

	// Update is called once per frame
	void Update () {
          
         //rotation, follow mouse pointer
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;
 
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
 
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // fire towards mouse
        //if (Input.GetMouseButtonDown(0)) {
        nextFire += Time.deltaTime;
        if (nextFire >= fireRate) {
            GameObject b = Instantiate(bullet, gun.transform.position+(1.3f*gun.transform.right), gun.transform.rotation) as GameObject;
            b.rigidbody2D.velocity = b.transform.right * 32;
            nextFire = 0.0f;
        }


    }
}
