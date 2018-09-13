using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour {

	private float m_detectionRange;
	Enemy enemy;
	[SerializeField] private Transform enemyTransform = null;
	private string s_player = "Player";

	private void Awake(){
		enemy = enemyTransform.GetComponent<Enemy> ();
		m_detectionRange = enemy.m_range;
		GetComponent<SphereCollider> ().radius = m_detectionRange;
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_player)) {
			enemy.isInRange = true;
			enemy.startedShooting = true;
		}
	}

	private void OnTriggerExit(Collider col){
		if (col.CompareTag (s_player)) {
			enemy.isInRange = false;
		}
	}
}
