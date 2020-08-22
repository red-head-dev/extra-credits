using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Mathf;

public class SceneControler: MonoBehaviour
{

	public GameObject exitDistanceText;
	public GameObject monsterDistanceText;
	public GameObject winText;
	public GameObject loseText;
	public GameObject endGameBackground;

	public float endGameDelay = 5.0f; // Seconds to wait after game end before returing to start

	private GameObject player;
	private Text m_exitDistanceText;
	private Text m_monsterDistanceText;
	private Text m_winText;
	private Text m_loseText;
	// Start is called before the first frame update
	void Start() {
		player = GameObject.FindWithTag("Player");

		m_exitDistanceText = exitDistanceText.GetComponent < Text > ();
		m_monsterDistanceText = monsterDistanceText.GetComponent < Text > ();
		m_winText = winText.GetComponent < Text > ();
		m_loseText = loseText.GetComponent < Text > ();

		m_winText.text = "";
		m_loseText.text = "";

		endGameBackground.SetActive(false);
	}


	// Update is called once per frame
	void Update() {
		if (Input.GetKey("escape"))
			Application.Quit();

		// Set distance
		m_exitDistanceText.text = "D = " +
		                          ClosestObjDist(player, GameObject.FindGameObjectsWithTag("Finish")).
		                          ToString("N2");

		m_monsterDistanceText.text = "M = " +
		                             ClosestObjDist(player, GameObject.FindGameObjectsWithTag("Monster")).
		                             ToString("N2");



	}

	static float ClosestObjDist(GameObject a, GameObject[] bs) {
		// Return distance from a to closest object in bs
		float dist = System.Single.MaxValue; // Max value of single precision float
		foreach (GameObject b in bs) {
			dist = Min(dist, Vector3.Distance(a.transform.position, b.transform.position));
		}
		return dist;
	}

	private IEnumerator ReloadAfterDelay() {
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
