using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class PlayerControlerReal: MonoBehaviour
{

	public GameObject sceneControler;
	public float forwardV = 1.0f;
	public float backwardV = 0.5f;
	public float acceleration = 1.0f;
	public float decceleration = 1.0f;
	public float xDecceleration = 15f;
	public float rotV = 180.0f; // degress /sec
	public float rotA = 1.0f; // degress /sec^2

	private Rigidbody2D rb;
	// Start is called before the first frame update
	void Start() {
		rb = GetComponent < Rigidbody2D > ();
	}

	private Vector2 a;
	private float omega = 0f;

	void Update() {



	}

	// Update is called once per frame
	void FixedUpdate() {

		// Use Input's sensistive so that movement is not instattanious
		// default to 0 to 100 in 1/3rd of a second.
		// RAW is just 1s or zeros not sure if good idea
		float vAxis = Input.GetAxisRaw("Vertical");

		{ // set velocity
			// need local
			Vector3 v = transform.InverseTransformVector(rb.velocity);
			//Debug.Log("local v= " + v );
			//
			//
			// IT would make sense to do the same type of using min and max as with
			// torque but I'm not going to bother now

			if ( vAxis > 0 ) { // forward
				if ( v.y < forwardV ) {
					a.y = acceleration * Time.fixedDeltaTime;
				} else if ( v.y > forwardV) {
					a.y = -decceleration * Time.fixedDeltaTime;
				} else {
					a.y = 0.0f;
				}
			} else if ( vAxis < 0 ) { // backwards
				if ( v.y < -backwardV ) {
					a.y = decceleration * Time.fixedDeltaTime;
				} else if ( v.y > -backwardV) {
					a.y = -decceleration * Time.fixedDeltaTime; // might make -acceleration
				} else {
					a.y = 0.0f;
				}
			} else { // stop
				if ( v.y < 0 ) {
					a.y = decceleration * Time.fixedDeltaTime;
				} else if ( v.y > 0) {
					a.y = -decceleration * Time.fixedDeltaTime;
				} else {
					a.y = 0.0f;
				}
			}
			// stop lateral movement
			if ( v.x < 0 ) {
				a.x = xDecceleration * Time.fixedDeltaTime;
			} else if ( v.x > 0) {
				a.x = -xDecceleration * Time.fixedDeltaTime;
			} else {
				a.x = 0.0f;
			}
		}

		{
			// Unity usess fucking degress in one part and radians in the other fuck you
			float curRotV = rb.angularVelocity;
			float desiredRotV = Input.GetAxis("Horizontal") * -rotV;
			//float desiredRotV = 1 * -rotV;

			float torque = rotA * PI / 180 * rb.inertia;
			if ( curRotV < desiredRotV ) { // Want to turn faster
				omega = Min(torque * Time.fixedDeltaTime, desiredRotV - curRotV);
			} else if ( curRotV > desiredRotV) { // Want to turn slower
				omega = Max(-torque * Time.fixedDeltaTime, desiredRotV - curRotV);
			} else {
				omega = 0.0f;
			}
		}
		//Debug.Log("Omega: " + omega);

		// 2D lacks acceleration this accelration is forces as long as mass = 1 KG
		// SOLUTION muliple force by mass
		//rb.AddRelativeForce(new Vector2(1f, 1f) * a, ForceMode2D.Force);
		//rb.AddTorque(omega, ForceMode2D.Force);

		rb.AddRelativeForce( a, ForceMode2D.Impulse);
		rb.AddTorque(omega, ForceMode2D.Impulse);

	}

	// On collision only works with a non trigger collider
	// trigger must be done in trigger.
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.gameObject.tag == "Finish" ) {
			Debug.Log("Entered Exit");
			sceneControler.GetComponent < SceneControlerReal > ().OnWin();
		}
		if (collision.collider.gameObject.tag == "Monster" ) {
			Debug.Log("Hit Monstert");
			sceneControler.GetComponent < SceneControlerReal > ().OnLose();
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Finish" ) {
			Debug.Log("Entered Exit");
			sceneControler.GetComponent < SceneControlerReal > ().OnWin();
		}
		if (collider.gameObject.tag == "Monster" ) {
			Debug.Log("Hit Monstert");
			sceneControler.GetComponent < SceneControlerReal > ().OnLose();
		}
	}
}
