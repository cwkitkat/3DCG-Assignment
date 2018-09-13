using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour {

	string s_player = "Player", s_win = "VictoryScene";

	void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_player)) {
			SceneManager.LoadScene (s_win);
		}
	}
}
