using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler: MonoBehaviour
{

	public GameObject exit;
	public GameObject sceneControler;
	public float forwardV = 1.0f;
	public float backwardV = 0.5f;
	public float acceleration = 1.0f;
	public float decceleration = 1.0f;
	public float xDecceleration = 15f;
	public float rotV = 180.0f; // degress /sec
	public float rotA = 1.0f; // degress /sec^2

	private Rigidbody2D m_Rigidbody2D;
	// Start is called before the first frame update
	void Start() {
		m_Rigidbody2D = GetComponent < Rigidbody2D > ();
	}

	private Vector2 a;
	private float omega = 0f;

	void Update() {

		// Use Input's sensistive so that movement is not instattanious
		// default to 0 to 100 in 1/3rd of a second.
		// RAW is just 1s or zeros not sure if good idea
		float vAxis = Input.GetAxisRaw("Vertical");

		{ // set velocity
			// need local
			Vector3 v = transform.InverseTransformVector(m_Rigidbody2D.velocity);
			Debug.Log("local v= " + v );

			if ( vAxis > 0 ) { // forward
				if ( v.y < forwardV ) {
					a.y = acceleration;
				} else if ( v.y > forwardV) {
					a.y = -decceleration;
				} else {
					a.y = 0.0f;
				}
			} else if ( vAxis < 0 ) { // backwards
				if ( v.y < -backwardV ) {
					a.y = decceleration;
				} else if ( v.y > -backwardV) {
					a.y = -decceleration; // might make -acceleration
				} else {
					a.y = 0.0f;
				}
			} else { // stop
				if ( v.y < 0 ) {
					a.y = decceleration;
				} else if ( v.y > 0) {
					a.y = -decceleration;
				} else {
					a.y = 0.0f;
				}
			}
			// stop lateral movement
			if ( v.x < 0 ) {
				Debug.Log("A");
				a.x = xDecceleration;
			} else if ( v.x > 0) {
				Debug.Log("B");
				a.x = -xDecceleration;
			} else {
				a.x = 0.0f;
			}
		}

		{
			float curRotV = m_Rigidbody2D.angularVelocity;
			float desiredRotV = Input.GetAxis("Horizontal") * -rotV;
			if ( curRotV < desiredRotV ) {
				omega = rotA;
			} else if ( curRotV > desiredRotV) {
				omega = -rotA;
			} else {
				omega = 0.0f;
			}
		}


	}

	// Update is called once per frame
	void FixedUpdate() {

		// tranform.up gives green axis in world units which is then multiplied by
		// 1 dimensional velocity to get the two dimensional velocity.

		// 2D lacks acceleration this accelration is forces as long as mass = 1 KG
		m_Rigidbody2D.AddRelativeForce(new Vector2(1f, 1f) * a, ForceMode2D.Force);
		//m_Rigidbody2D.velocity = transform.up * velocityV;
		m_Rigidbody2D.AddTorque(omega, ForceMode2D.Force);
	}

	// On collision only works with a non trigger collider
	// trigger must be done in trigger.
	void OnCollisionEnter2D(Collision2D collision) {
		Debug.Log("colide");
		if (collision.collider.gameObject == exit ) {
			Debug.Log("enter");
			sceneControler.GetComponent < SceneControler > ().OnWin();
		}
	}
}
