using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControler: MonoBehaviour
{

	public GameObject player;
	public GameObject exit;
	public GameObject exitDistanceText;
	public GameObject winText;
	public GameObject loseText;

	public float endGameDelay = 5.0f; // Seconds to wait after game end before returing to start

	private Text m_exitDistanceText;
	private Text m_winText;
	private Text m_loseText;
	// Start is called before the first frame update
	void Start() {
		m_exitDistanceText = exitDistanceText.GetComponent < Text > ();
		m_winText = winText.GetComponent < Text > ();
		m_loseText = loseText.GetComponent < Text > ();

		m_winText.text = "";
		m_loseText.text = "";
	}


	// Update is called once per frame
	void Update() {
		if (Input.GetKey("escape"))
			Application.Quit();

		// Set distance
		m_exitDistanceText.text = "D = " +
		                          Vector3.Distance(player.transform.position, exit.transform.position).
		                          ToString("N2");

	}

	private IEnumerator ReloadAfterDelay() {
		yield return new WaitForSeconds(endGameDelay);
		SceneManager.LoadScene("StartScene");
	}

	public void OnWin() {
		m_winText.text = "You Won!";
		StartCoroutine(ReloadAfterDelay());
	}


	public void OnLose() {
		m_loseText.text = "You Lost!";
		StartCoroutine(ReloadAfterDelay());
	}
}
