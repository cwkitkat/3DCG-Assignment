using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

	[SerializeField] private GameObject countdownObj = null;
	private Text countdown = null;
	private int m_countdown;

	public void GoToMainMenu(){
		SceneManager.LoadScene (0);
	}

	public void GoToLevel(int i){
		SceneManager.LoadScene (i);
	}

	public void RestartLevel(){
		int i = SceneManager.GetActiveScene ().buildIndex;
		GoToLevel (i);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void PauseGame(){
		Time.timeScale = 0;
	}

	public void ResumeGame(){
		Time.timeScale = 1;
	}

	private void OnEnable(){
		//GameOver
		if (countdownObj != null) {
			countdown = countdownObj.GetComponent<Text> ();
			StartCoroutine (CountingDown ());
		}
	}

	private IEnumerator CountingDown(){
		m_countdown = 10;
		while (m_countdown > 0) {
			m_countdown--;
			countdown.text = m_countdown.ToString();
			yield return new WaitForSeconds(1);
		}
		yield return new WaitForSeconds(1);
		GoToMainMenu ();
	}
}
