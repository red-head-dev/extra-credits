using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Mathf;
using System;

/// Push ESC to quit game
public class EscToQuit: MonoBehaviour
{
	// Update is called once per frame
	void Update() {
		if (Input.GetKey("escape"))
			Application.Quit();
	}
}
