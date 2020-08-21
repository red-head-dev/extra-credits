using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject: MonoBehaviour
{

	public GameObject target;

	// Update is called once per frame
	void Update() {
		// Update 2d pos to follow target
    // Horrible should use position transform.position
		transform.SetPositionAndRotation(new Vector3( target.transform.localPosition.x,
		                                              target.transform.localPosition.y,
		                                              transform.localPosition.z),
		                                 transform.rotation);
	}
}
