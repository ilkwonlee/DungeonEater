using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToTitleScript : MonoBehaviour {
	void start() {

	}

	void Update() {

	}

	public void ChangeScene() {
		SceneManager.LoadScene("TitleScene");
	}
}
