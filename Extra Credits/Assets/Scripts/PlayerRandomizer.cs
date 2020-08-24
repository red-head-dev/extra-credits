using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandomizer: MonoBehaviour
{
	public Transform [] startingPoints;
	// Start is called before the first frame update
	void Start() {
		GameObject p = GameObject.FindWithTag("Player");
		Transform t = startingPoints[RandomWholeNum(startingPoints.Length)];
		p.transform.position = t.position;
		p.transform.rotation = t.rotation;

	}

	int RandomWholeNum(int max) {
		// Max is exclsive
		return Mathf.FloorToInt(Random.value * max);
	}

}
