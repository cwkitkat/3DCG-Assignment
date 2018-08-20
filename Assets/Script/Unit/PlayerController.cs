using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	[SerializeField] protected GameObject mainCam;

	PlayerStats playerStatus;

	private Rigidbody m_rb;
	private string s_horizontal = "Horizontal", s_vertical = "Vertical";
	private float m_speed;
	private Vector3 moveInput;
	private Vector3 movement;

	private void OnEnable(){
		m_rb = GetComponent<Rigidbody> ();
		m_rb.constraints = RigidbodyConstraints.FreezeRotation;

		SetUpCam ();

		playerStatus = GetComponent<PlayerStats> ();
	}

	private void Update(){
		if (playerStatus.isActivated) {
			PlayerInput ();
		}
	}

	private void FixedUpdate(){
		if (playerStatus.isActivated) {
			MovePlayer ();
		}
	}

	#region Camera Set Up

	private void SetUpCam(){
		mainCam.transform.position = new Vector3 (transform.position.x + 0f, transform.position.y + 3f, transform.position.z - 6f);
		mainCam.transform.rotation = Quaternion.Euler (1.35f, 0, 0);
		mainCam.transform.SetParent (transform);
	}

	#endregion

	#region Move

	private void PlayerInput(){
		moveInput = new Vector3 (Input.GetAxisRaw (s_horizontal), 0f, Input.GetAxisRaw (s_vertical));
	}

	private void MovePlayer(){
		m_speed = playerStatus.speed * Time.deltaTime;
		movement = moveInput * m_speed;
		m_rb.MovePosition (transform.position + movement);
	}

	#endregion

	#region Shoot
	//help
	private void Aim(){
		
	}

	private void Shoot(){
		
	}

	//want reload?
	#endregion
}
