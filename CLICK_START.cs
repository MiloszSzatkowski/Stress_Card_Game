using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CLICK_START : MonoBehaviour {

	public Button START;

	// Use this for initialization
	void Start () {
			 gameObject.GetComponent<Button>().onClick.AddListener(StartGame);
	}

	public void StartGame () {
		SceneManager.LoadScene("__GAME__");
	}

}
