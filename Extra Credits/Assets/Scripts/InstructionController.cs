using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Mathf;
using System;

public class InstructionController: MonoBehaviour
{
	public String nextScene;
	public float endGameDelay = 5.0f; // Seconds to wait after till next scene

	// Start is called before the first frame update
	void Start() {
		StartCoroutine(ReloadAfterDelay());
	}

	private IEnumerator ReloadAfterDelay() {
		yield return new WaitForSeconds(endGameDelay);
		GetComponent < MusicController > ().StopMusic();
		SceneManager.LoadScene(nextScene);
	}
}
