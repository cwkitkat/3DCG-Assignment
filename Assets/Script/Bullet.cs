using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag ("Enemy")) {
			//decrease hp or kill enemy
			col.gameObject.GetComponent<EnemyStats>().health -= 1;
			gameObject.SetActive (false);
		} else {
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