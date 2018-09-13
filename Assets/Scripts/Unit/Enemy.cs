using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

	public Transform m_player;
	[SerializeField] protected Transform[] waypoints = null;
	public float m_hp;
	[SerializeField] private float currentHp = 0;
	public float m_range;
	[HideInInspector] public bool isInRange;
	[SerializeField] private GameObject gun = null;
	[SerializeField] private GameObject bullet = null;
	private List<GameObject> bulletList = new List<GameObject> ();
	[SerializeField] private int bulletCount = 10;
	[SerializeField] Transform gunBarrel = null;
	[SerializeField] private float bulletForce = 5;
	[SerializeField] private float shootInterval = 3f;
	private bool isAlive;
	private bool gainScore;
	private Rigidbody m_rb;
	[SerializeField] private AudioSource sfxSource = null;
	[SerializeField] private ParticleSystem ps_explode = null;
	private ParticleSystem m_ps;

	public void DetectPlayer(Transform playerTransform){
		m_player = playerTransform;
	}

	private void Awake(){
		hp = m_hp;
		ShowCurrentHealth ();
		BulletObjectPool ();
		m_rb = GetComponent<Rigidbody> ();
		gameObject.SetActive (false);
		m_ps = Instantiate (ps_explode);
		m_ps.Stop ();
	}

	private void Update(){
		if (isAlive) {
			TargetPlayer ();
		}
		if (hp > 0) {
			isAlive = true;
		} else {
			isAlive = false;
			if (!gainScore) {
				m_ps.transform.position = transform.position;
				m_ps.Play ();
				AudioManager.audioManager.PlaySfx (AudioManager.Sfx.explode);
				gainScore = true;
				Player.score++;
				m_rb.useGravity = true;
			}
		}
	}

	private void BulletObjectPool(){
		for (int i = 0; i < bulletCount; i++) {
			GameObject m_bullet = Instantiate (bullet, transform.position, transform.rotation);
			bulletList.Add (m_bullet);
			m_bullet.SetActive (false);
		}
	}

	private Vector3 targetLocation;
	[HideInInspector] public bool startedShooting;
	private Coroutine co_shooting;

	protected virtual void TargetPlayer(){
		if (isInRange && startedShooting) {
			co_shooting = StartCoroutine (ShootRoutine ());
			startedShooting = false;
		} 
		if (isInRange) {
			transform.LookAt (m_player);
		}
		if(!isInRange) {
			if (co_shooting != null) {
				StopCoroutine (co_shooting);
			}
		}
	}

	private IEnumerator ShootRoutine(){
		float randomStartingTime = Random.Range (0.1f, 2f);
		yield return new WaitForSeconds (shootInterval/2 + randomStartingTime);
		while (isInRange && isAlive && Time.timeScale == 1) {
			gun.transform.LookAt (m_player);
			targetLocation = m_player.position;
			ShootPlayer ();
			yield return new WaitForSeconds (shootInterval);
		}
		yield return null;
	}

	private void ShootPlayer(){
		//select bullet
		GameObject m_bullet = null;
		for (int i = 0; i < bulletList.Count; i++) {
			if (!bulletList [i].activeInHierarchy) {
				m_bullet = bulletList [i];
				break;
			}
		}
		if (m_bullet != null) {
			m_bullet.transform.position = gunBarrel.transform.position;
			m_bullet.SetActive (true);
			Rigidbody rb_bullet = m_bullet.GetComponent<Rigidbody> ();
			rb_bullet.velocity = Vector3.zero;
			sfxSource.Play();
			rb_bullet.AddForce ((targetLocation - m_bullet.transform.position) * bulletForce, ForceMode.Impulse);
		}
	}

	protected virtual void DamageMe(){
		hp--;
		ShowCurrentHealth ();
	}

	private void ShowCurrentHealth(){
		currentHp = hp;
	}
}
