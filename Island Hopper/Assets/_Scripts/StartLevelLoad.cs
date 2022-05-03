using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartLevelLoad : MonoBehaviour {

	public string nameOfLevelToLoad  = "";

	// Use this for initialization
	void Start () {
		SceneManager.LoadScene(nameOfLevelToLoad, LoadSceneMode.Single);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
