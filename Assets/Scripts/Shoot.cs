using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject bulletPrefab;
	private GameObject bullet;
	private Rigidbody rb_bullet;
	[SerializeField] private Transform weaponLocation = null;
	[SerializeField] private float zOffset_weapon = .1f; 
	private Vector3 mousePos;
	public LayerMask layer_IgnoreRaycast;
	[SerializeField] private float bulletForce = 5;
	[SerializeField] private int bulletCount = 8;
	private List<GameObject> bulletInScene = null;

	private void Awake(){
		bulletInScene = new List<GameObject> ();
	}

	private void Start(){
		InitiateBullet ();
		StartCoroutine (Shooting());
	}

	private void InitiateBullet(){
		for (int i = 0; i < bulletCount; i++) {
			bullet = Instantiate (bulletPrefab, transform.position, transform.rotation);
			//bullet.transform.SetParent (gameObject.transform);
			rb_bullet = bullet.GetComponent<Rigidbody> ();
			bullet.SetActive (false);
			bulletInScene.Add (bullet);
		}
	}

	private IEnumerator Shooting(){
		while (Time.timeScale == 1) { //set other condition maybe**
			mousePos = Input.mousePosition;
			if(Input.GetButtonDown("Fire1")){
				ShootBullet (bulletForce);
			}
			Ray castPoint = Camera.main.ScreenPointToRay (mousePos);
			RaycastHit hit;
			if (Physics.Raycast (castPoint, out hit, 100f)) {
				weaponLocation.LookAt (hit.point);
				Debug.DrawLine (castPoint.origin, hit.point, Color.green);
			}
			yield return null;
		}
		yield return null;
	}

	private void ShootBullet(float force){
		for (int i = 0; i < bulletInScene.Count; i++) {
			if (!bulletInScene [i].activeInHierarchy) {
				GameObject bullet = bulletInScene [i];
				Rigidbody rb_bullet = bullet.GetComponent<Rigidbody> ();
				bullet.SetActive (true);
				bullet.transform.position = weaponLocation.position + Vector3.forward * zOffset_weapon;
				rb_bullet.velocity = Vector3.zero;
				Ray castPoint = Camera.main.ScreenPointToRay (mousePos);
				RaycastHit targetShot;
				if (Physics.Raycast (castPoint, out targetShot, Mathf.Infinity, layer_IgnoreRaycast)) {
					rb_bullet.AddForce ((targetShot.point - weaponLocation.position) * force, ForceMode.Force);
				}
				break;
			}
		}
	}
}
