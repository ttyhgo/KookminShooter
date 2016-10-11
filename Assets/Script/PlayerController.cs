using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	Rigidbody rb;
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire;

	private AudioSource fireAudio;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		fireAudio = GetComponent<AudioSource> ();
	}

	void Update(){
		if (Input.GetMouseButton (0)) {
			print ("clicked");
		}
		if (Input.GetKey (KeyCode.Space) && Time.time > nextFire) {
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			fireAudio.Play ();
			nextFire = Time.time + fireRate;
		}
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		rb.velocity = new Vector3 (moveHorizontal, 0, moveVertical) * speed;
		rb.position = new Vector3 (
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax)
			,0
			,Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0, 0, -rb.velocity.x*tilt);
	}
}
