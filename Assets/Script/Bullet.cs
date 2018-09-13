using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private void OnTriggerEnter(Collider col){
//		if (col.CompareTag ("Enemy")) {
//			//decrease hp or kill enemy
//			//rb_bullet.velocity = Vector3.zero; ?
//			//col.gameObject.GetComponent<EnemyStats>().health -= 1;
//			gameObject.SetActive (false);
//		} else {
//			gameObject.SetActive (false);
//		}
		if (col.CompareTag ("Player")) {
			//decrease hp or kill enemy
			//rb_bullet.velocity = Vector3.zero; ?
			//col.gameObject.GetComponent<EnemyStats>().health -= 1;
			gameObject.SetActive (false);
		} 
		if(col.CompareTag("Environment")) {
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