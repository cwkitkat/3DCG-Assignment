using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour {
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

		StartCoroutine (FollowWaypoints());
	}
	private void Update(){
		if (playerFound) {
			transform.LookAt (player.transform);
		}
	}
	bool clockwise = false;
	float timer = 6;
	private IEnumerator FollowWaypoints(){
		yield return new WaitForSeconds (2f);
		playerFound = true;
		while (playerStatus.isActivated && playerStatus.health > 0) {
			if ((transform.position - player.transform.position).magnitude <= 4.5f) {
				
				if (clockwise && timer > 0) {
					transform.RotateAround (player.transform.position, player.transform.up, 45f * Time.deltaTime);
					timer -= Time.deltaTime;
					if (timer < 0) {
						clockwise = false;
						timer = Random.Range(4,8);
					}
				} else if (!clockwise && timer > 0) {
					transform.RotateAround (player.transform.position, player.transform.up, -45f * Time.deltaTime);
					timer -= Time.deltaTime;
					if (timer < 0) {
						clockwise = true;
						timer = Random.Range(4,8);
					}
				}
			} else {
				transform.position = Vector3.MoveTowards (transform.position, player.transform.position, enemyStatus.speed * Time.deltaTime);
			}
			yield return null;
		}
		yield return null;
	}
}
