using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

	public Transform player;
	[SerializeField] protected Transform[] waypoints = null;
	public int m_hp;
	[SerializeField] private int currentHp = 0;
	public float m_range;
	[HideInInspector] public bool isInRange;
	[SerializeField] private GameObject gun = null;
	[SerializeField] private GameObject bullet = null;
	private List<GameObject> bulletList = new List<GameObject> ();
	[SerializeField] private int bulletCount = 10;
	[SerializeField] Transform gunBarrel = null;
	[SerializeField] private float bulletForce = 5;
	[SerializeField] private float shootInterval = 3f;

	public void DetectPlayer(Transform playerTransform){
		player = playerTransform;
	}

	private void Awake(){
		hp = m_hp;
		ShowCurrentHealth ();
		BulletObjectPool ();

		gameObject.SetActive (false);
	}

	private void OnEnable(){
		
	}

	private void Update(){
		TargetPlayer ();
	}

	protected virtual void MoveLeftAndRight(){
	}

	protected virtual void AimPlayer(){
		//laser thing
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
	//use in Update later**
	protected virtual void TargetPlayer(){
		if (isInRange && startedShooting) {
			co_shooting = StartCoroutine (ShootRoutine ());
			startedShooting = false;
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
		while (isInRange) {
			gun.transform.LookAt (player);
			targetLocation = player.position;
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
			}
		}
		if (m_bullet != null) {
			m_bullet.transform.position = gunBarrel.transform.position;
			m_bullet.SetActive (true);
			Rigidbody rb_bullet = m_bullet.GetComponent<Rigidbody> ();
			rb_bullet.velocity = Vector3.zero;
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
