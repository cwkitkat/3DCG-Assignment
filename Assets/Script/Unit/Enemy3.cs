using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour {

	EnemyStats enemyStatus;

	private Rigidbody m_rb;

	private GameObject player;
	PlayerStats playerStatus;

	//
	private bool playerFound;

	private void OnEnable(){
		m_rb = GetComponent<Rigidbody> ();

		enemyStatus = GetComponent<EnemyStats> ();

		if (player == null) {
			player = GameObject.Find ("Player");
			playerStatus = player.GetComponent<PlayerStats> ();
		}

		StartCoroutine (RunAwayFromPlayer());
	}
	private void Update(){
		if (playerFound) {
			transform.LookAt (player.transform);
		}
	}

	private IEnumerator RunAwayFromPlayer(){
		yield return new WaitForSeconds (2f);
		playerFound = true;
		while (playerStatus.isActivated && playerStatus.health > 0) {
			if ((transform.position - player.transform.position).magnitude <= 12f) {
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position, -enemyStatus.speed * Time.deltaTime);
			}
			yield return null;
		}
		yield return null;
	}
}
