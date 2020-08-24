using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterAi: MonoBehaviour
{
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void OnPathComplete(Path p) {
		Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

	}
}
