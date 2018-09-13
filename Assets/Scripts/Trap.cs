using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	private string s_player = "Player";
	[SerializeField] private GameObject[] enemies = new GameObject[2];
	[SerializeField] private Transform[] type1 = null, type2 = null;
	private List<GameObject> enemySpawned = new List<GameObject>();
	private List<Enemy> m_enemy = new List<Enemy>();
	private Collider m_col;

	private void Awake(){
		m_col = GetComponent<Collider> ();
		SpawnEnemies();
	}

	private void SpawnEnemies(){
		SpawnEnemy (type1, enemies[0]);
		SpawnEnemy (type2, enemies[1]);
	}

	private void SpawnEnemy(Transform[] type, GameObject enemyType){
		if (enemyType != null) {
			foreach (Transform spawner in type) {
				GameObject tempEnemy = Instantiate (enemyType, spawner.transform.position, spawner.transform.rotation);
				enemySpawned.Add (tempEnemy);
				m_enemy.Add (tempEnemy.GetComponent<Enemy> ());
			}
		}
	}

	private void OnTriggerEnter(Collider col){
		if (col.CompareTag (s_player)) {
			ActivateTrap(col.transform);
			m_col.enabled = false;
		}
	}

	private void ActivateTrap(Transform player){
		foreach (Enemy temp_m_enemy in m_enemy) {
			temp_m_enemy.DetectPlayer (player);
		}
		foreach (GameObject enemy in enemySpawned) {
			enemy.SetActive(true);
		}
	}
}
