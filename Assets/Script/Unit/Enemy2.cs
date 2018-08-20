using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {

	EnemyStats enemyStatus;

	private Rigidbody m_rb;

	private GameObject player;
	PlayerStats playerStatus;

	//
	private bool playerFound;
	[SerializeField] private float attackCooldown = 0.65f;

	private void OnEnable(){
		m_rb = GetComponent<Rigidbody> ();

		enemyStatus = GetComponent<EnemyStats> ();

		if (player == null) {
			player = GameObject.Find ("Player");
			playerStatus = player.GetComponent<PlayerStats> ();
		}

		StartCoroutine (ChasePlayer());
	}
	private void Update(){
		if (playerFound)
			transform.LookAt (player.transform);
	}

	private IEnumerator ChasePlayer(){
		yield return new WaitForSeconds (2f);
		playerFound = true;
		while (playerStatus.isActivated && playerStatus.health > 0) {
			transform.LookAt (player.transform);
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, enemyStatus.speed * Time.deltaTime);
			if ((transform.position - player.transform.position).magnitude <= 1.5f) {
				//attack
				yield return new WaitForSeconds (attackCooldown);
				float backwardsTime = attackCooldown * 2;
				while (backwardsTime > 0) {
					transform.position = Vector3.MoveTowards (transform.position, -player.transform.position, enemyStatus.speed * Time.deltaTime);
					backwardsTime -= Time.deltaTime;
					yield return null;
				}
			}
				yield return null;

		}
		yield return null;
	}
}
