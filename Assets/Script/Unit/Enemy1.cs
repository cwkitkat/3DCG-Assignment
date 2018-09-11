using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {

	EnemyStats enemyStatus;

	private Rigidbody m_rb;

	private GameObject player;
	PlayerStats playerStatus;

	[SerializeField] private float delay = 1; 
	[SerializeField] private float attackCooldown = 1.5f;

	private void OnEnable(){
		m_rb = GetComponent<Rigidbody> ();

		enemyStatus = GetComponent<EnemyStats> ();

		if (player == null) {
			player = GameObject.Find ("Player");
			playerStatus = player.GetComponent<PlayerStats> ();
		}

		StartCoroutine (ChasePlayer());
	}

	private IEnumerator ChasePlayer(){
		yield return new WaitForSeconds (2f);
		int count = 0;
		while (enemyStatus.health > 0 && playerStatus.isActivated && playerStatus.health > 0) {
			transform.LookAt (player.transform);
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, enemyStatus.speed * Time.deltaTime);
			count++;
			if ((transform.position - player.transform.position).magnitude <= 1.5f) {
				//attack
				yield return new WaitForSeconds (attackCooldown);
			}
			if (count % 20 == 0) {
				yield return new WaitForSeconds (delay);
			} else {
				yield return null;
			}
		}
		yield return null;
	}
}
