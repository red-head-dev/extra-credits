using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler: MonoBehaviour
{

	public GameObject exit;
	public GameObject sceneControler;
	public float forwardV = 1.0f;
	public float backwardV = 0.5f;
	public float rotV = 1.0f;

	private Rigidbody2D m_Rigidbody2D;
	// Start is called before the first frame update
	void Start() {
		m_Rigidbody2D = GetComponent < Rigidbody2D > ();
	}

	private float velocityV = 0f;
	private float angularV = 0f; // degrees per second

	void Update() {

		// Use Input's sensistive so that movement is not instattanious
		// default to 0 to 100 in 1/3rd of a second.
		float vAxis = Input.GetAxis("Vertical");

		velocityV = 0f; // Hopefully no threading issues
		if ( vAxis > 0 )
			velocityV = vAxis * forwardV;
		if ( vAxis < 0 )
			velocityV = vAxis * backwardV;

		angularV = Input.GetAxis("Horizontal") * -rotV;
	}

	// Update is called once per frame
	void FixedUpdate() {

		// tranform.up gives green axis in world units which is then multiplied by
		// 1 dimensional velocity to get the two dimensional velocity.
		m_Rigidbody2D.velocity = transform.up * velocityV;
		m_Rigidbody2D.angularVelocity = angularV;
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
