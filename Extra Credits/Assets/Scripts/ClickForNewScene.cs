using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickForNewScene: MonoBehaviour
{
	public void NextScene(string scene) {
		// Switch to scene specified by string.
		SceneManager.LoadScene(scene);
	}

}
