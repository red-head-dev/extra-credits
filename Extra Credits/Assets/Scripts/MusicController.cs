using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Mathf;
using System;

public class MusicController: MonoBehaviour
{

	public GameObject exitDistanceText;
	public GameObject monsterDistanceText;
	public GameObject endGameBackground;


	private GameObject player;
	private Text m_exitDistanceText;
	private Text m_monsterDistanceText;

	// Peter FMOD code
	private FMOD.Studio.EventInstance instance;
	[FMODUnity.EventRef]
	public string fmodEvent;

	public float distCoeff = 60.0f; // How close you need to be for music layers to play
	[SerializeField][Range(0f, 1f)]
	private float MonParam;
	[SerializeField][Range(0f, 1f)]
	private float exitParam;
	// End Peter FMOD code


	// Start is called before the first frame update
	void Start() {
		player = GameObject.FindWithTag("Player");

		m_exitDistanceText = exitDistanceText.GetComponent < Text > ();
		m_monsterDistanceText = monsterDistanceText.GetComponent < Text > ();

		// Peter FMOD code
		instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
		instance.start(); // DOES NOT RETURN!!!!
		// End Peter FMOD code
		Debug.Log("END OF START");

		instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

		instance.start();
	}

	void OnDestory() {
		StopMusic();
	}

	public void StopMusic() {
		instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
	}

	// Update is called once per frame
	void Update() {
		// Set distance
		m_exitDistanceText.text = "D = " +
		                          ClosestObjDist(player, GameObject.FindGameObjectsWithTag("Finish")).
		                          ToString("N2");

		m_monsterDistanceText.text = "M = " +
		                             ClosestObjDist(player, GameObject.FindGameObjectsWithTag("Monster")).
		                             ToString("N2");

		// Peter FMOD code
		MonParam = ConvertDistToFMOD((double)ClosestObjDist(player, GameObject.FindGameObjectsWithTag("Monster")));
		exitParam = ConvertDistToFMOD((double)ClosestObjDist(player, GameObject.FindGameObjectsWithTag("Finish")));
		instance.setParameterByName("MonParam", MonParam);
		instance.setParameterByName("ExitParam", exitParam);
		// End Peter FMOD code

	}

	// Peter FMOD code
	float ConvertDistToFMOD(double dist) {
		// Return converion from spatial distance to FMOD parameter
		float FMODparam = 0;
		if (dist < distCoeff) {
			FMODparam = (float)Math.Sqrt((1 - Math.Pow(dist / distCoeff, 2)));
		}
		return FMODparam;
	}
	// End Peter FMOD code

	static float ClosestObjDist(GameObject a, GameObject[] bs) {
		// Return distance from a to closest object in bs
		float dist = System.Single.MaxValue; // Max value of single precision float
		foreach (GameObject b in bs) {
			dist = Min(dist, Vector3.Distance(a.transform.position, b.transform.position));
		}
		return dist;
	}


}
