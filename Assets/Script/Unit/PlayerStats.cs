using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Units {

	[Header("Adjust Player Settings")]
	[SerializeField] private float activateDelay = 2f;
	[SerializeField] private float playerHealth = 5;
	[SerializeField] private float movementSpeed = 10;
//	[Header("In-game Stats")]
//	public float m_health;
//	public float m_speed;

	private void Awake(){
		SetPlayerStats ();
	}

	private void Start(){
		StartCoroutine (ActivatePlayer ());
		Invoke ("test", 6);
	}

	private void SetPlayerStats(){
		health = playerHealth;
		speed = movementSpeed;

//		m_health = health;
//		m_speed = movementSpeed;
	}

	private IEnumerator ActivatePlayer(){
		yield return new WaitForSeconds (activateDelay);
		isActivated = true;
		DisplayStats ();
		yield return null;
	}

	private void DisplayStats(){
		if (isActivated) {
			Debug.Log ("Player: " + "HP = " + health + ", " + "Speed = " + speed);
		}
	}

	private void test(){
		TakeDamage (1);
	}

	//For damage use
	public void TakeDamage(float damage){
		float initialHealth = health; 
		health -= damage;
		if (health < initialHealth) {
			//show effects
			DisplayStats();
		}
//		m_health = health;
	}
}
