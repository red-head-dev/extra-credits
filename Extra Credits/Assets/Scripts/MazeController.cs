using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Mathf;
using System;

public class MazeController: MonoBehaviour
{

	public GameObject winText;
	public GameObject loseText;
	public GameObject endGameBackground;

	public float endGameDelay = 5.0f; // Seconds to wait after game end before returing to start

	private Text m_exitDistanceText;
	private Text m_monsterDistanceText;
	private Text m_winText;
	private Text m_loseText;

	private MusicController musicController;

	// Start is called before the first frame update
	void Start() {
		musicController = GetComponent < MusicController > ();
		m_winText = winText.GetComponent < Text > ();
		m_loseText = loseText.GetComponent < Text > ();

		m_winText.text = "";
		m_loseText.text = "";
		endGameBackground.SetActive(false);

	}

	// Update is called once per frame
	void Update() {
	}

	private IEnumerator ReloadAfterDelay() {
		musicController.StopMusic();
		yield return new WaitForSeconds(endGameDelay);
		SceneManager.LoadScene("StartScene");
	}

	public void OnWin() {
		endGameBackground.SetActive(true);
		m_winText.text = "You Won!";
		StartCoroutine(ReloadAfterDelay());
	}


	public void OnLose() {
		endGameBackground.SetActive(true);
		m_loseText.text = "You Lost!";
		StartCoroutine(ReloadAfterDelay());
	}
}
