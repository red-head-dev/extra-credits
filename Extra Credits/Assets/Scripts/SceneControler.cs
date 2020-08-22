using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControler: MonoBehaviour
{

	public GameObject player;
	public GameObject exit;
	public GameObject exitDistanceText;
	public GameObject winText;
	public GameObject loseText;

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

		// Set distance
		m_exitDistanceText.text = "D = " + Vector3.Distance(player.transform.position, exit.transform.position);

	}

	public void OnWin() {
		m_winText.text = "You Won!";
	}

	public void OnLose() {
		m_loseText.text = "You Lost!";
	}
}
