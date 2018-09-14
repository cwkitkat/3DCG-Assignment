using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : Unit {

	public float m_hp;
	[SerializeField] private float currentHp = 0;
	[SerializeField] private Slider hpSlider = null;
	public static int score = 0;
	[SerializeField] private GameObject[] blockage = new GameObject[3];
	FirstPersonController fpsCtrl;
	private string m_player = "Player", m_untag = "Untagged";
	[SerializeField] private GameObject gameOverPanel = null;

	private void Awake(){
		score = 0;
		hp = m_hp;
		BulletObjectPool ();
		fpsCtrl = GetComponent<FirstPersonController> ();
	}

	private void Update(){
		ShowHealth ();
		Shoot ();
		ScoreEvent ();
		HealthEvent ();
	}

	private void ScoreEvent(){
		if (score == 10) {
			blockage [0].SetActive (false);
		}
		if (score == 12) {
			blockage [1].SetActive (false);
		}
		if (score == 22) {
			blockage [2].SetActive (false);
		}
	}

	private void ShowHealth(){
		hpSlider.value = hp / m_hp;
		currentHp = hp;
	}

	private void HealthEvent(){
		if (hp <= 0) {
			fpsCtrl.enabled = false;
			transform.tag = m_untag;
			gameOverPanel.SetActive (true);
//			Cursor.visible = true;
//			Cursor.lockState = CursorLockMode.None;
//			Cursor.lockState = CursorLockMode.Confined;
		}
	}

	public void Revive(){
		hp = m_hp;
		fpsCtrl.enabled = true;
		transform.tag = m_player;
//		Cursor.visible = false;
//		Cursor.lockState = CursorLockMode.Locked;
	}

	[SerializeField] private Transform gun = null;
	[SerializeField] private GameObject bullet = null;
	private List<GameObject> bulletList = new List<GameObject> ();
	[SerializeField] private int bulletCount = 10;
	[SerializeField] private float bulletForce = 5;
	private string s_Fire1 = "Fire1";

	private void BulletObjectPool(){
		for (int i = 0; i < bulletCount; i++) {
			GameObject m_bullet = Instantiate (bullet, transform.position, transform.rotation);
			bulletList.Add (m_bullet);
			m_bullet.SetActive (false);
		}
	}

	private void Shoot(){
		if (Time.timeScale == 1) {
			if (Input.GetButtonDown (s_Fire1) && hp > 0) {
				ShootBullet ();
			}
		}
	}

	private void ShootBullet(){
		GameObject m_bullet = null;
		for (int i = 0; i < bulletList.Count; i++) {
			if (!bulletList [i].activeInHierarchy) {
				m_bullet = bulletList [i];
				break;
			}
		}
		if (m_bullet != null) {
			m_bullet.transform.position = gun.transform.position;
			m_bullet.SetActive (true);
			Rigidbody rb_bullet = m_bullet.GetComponent<Rigidbody> ();
			rb_bullet.velocity = Vector3.zero;
			rb_bullet.AddForce ((transform.forward) * bulletForce, ForceMode.Impulse);
			AudioManager.audioManager.PlaySfx (AudioManager.Sfx.gunShot);
		}
	}
}
