using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Units {

	[Header("Adjust Enemy Settings")]
	//ID 0 means enemy die?
	[SerializeField] [Range(1,3)]private int enemyID = 0;

	[Header("Enemy 1")]
	[SerializeField] private float enemy1_Health = 3;
	[SerializeField] private float enemy1_Speed = 5;

	[Header("Enemy 2")]
	[SerializeField] private float enemy2_Health = 5;
	[SerializeField] private float enemy2_Speed = 2;

	[Header("Enemy 3")]
	[SerializeField] private float enemy3_Health = 2;
	[SerializeField] private float enemy3_Speed = 3.5f;

	private void Awake(){
		SetEnemyStats ();
	}

	private void OnEnable(){
		isActivated = true;
		DisplayStats ();
	}

	private void OnDisable(){
		isActivated = false;
	}

	private void SetEnemyStats(){
		if (enemyID == 1) {
			health = enemy1_Health;
			speed = enemy1_Speed;
		}
		else if (enemyID == 2) {
			health = enemy2_Health;
			speed = enemy2_Speed;
		}
		else if (enemyID == 3) {
			health = enemy3_Health;
			speed = enemy3_Speed;
		}
	}

	private void DisplayStats(){
		if (isActivated) {
			Debug.Log ("Enemy: " + "HP = " + health + ", " + "Speed = " + speed);
		}
	}
}
