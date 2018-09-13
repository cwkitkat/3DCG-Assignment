using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Player : MonoBehaviour {
	private string s_enemy = "Enemy", s_environment = "Environment";

	private void Awake(){
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_enemy)) {
			col.gameObject.GetComponent<Enemy> ().hp --;
			gameObject.SetActive (false);
		}
		if(col.CompareTag(s_environment)) {
			gameObject.SetActive (false);
		}
	}

	private void OnEnable(){
		StartCoroutine (Disappear());
	}

	private IEnumerator Disappear(){
		yield return new WaitForSeconds (2);
		gameObject.SetActive (false);
		yield return null;
	}
}
