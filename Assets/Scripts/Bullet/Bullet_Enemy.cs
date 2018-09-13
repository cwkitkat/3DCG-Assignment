using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Enemy : MonoBehaviour {
	Player player;
	private string s_player = "Player", s_environment = "Environment";

	private void Awake(){
		player = GameObject.FindGameObjectWithTag(s_player).GetComponent<Player> ();
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_player)) {
			player.hp--;
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
