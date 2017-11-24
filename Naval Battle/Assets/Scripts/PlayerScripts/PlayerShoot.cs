using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour {

	[System.Serializable]
	private class ShootingPoint {

		public Transform Cannon;
		[HideInInspector]
		public bool IsReloading = false;
	}

	[SerializeField]
	private GameObject CannonBall;
	[SerializeField]
	private List<ShootingPoint> LeftShootingPoints = new List<ShootingPoint>();
	[SerializeField]
	private List<ShootingPoint> RightShootingPoints = new List<ShootingPoint>();
	[SerializeField]
	private float CannonReloadTime = 2.0f;
	[SerializeField]
	private float MinPower = 1000.0f;
	[SerializeField]
	private float MaxPower = 1500.0f;
	[SerializeField]
	private AudioSource CannonSound;
	[SerializeField]
	private AudioClip CannonClip;

	private Transform _transform;
	private Transform _camera;

	private void Awake() {
		_transform = transform;
		_camera = GameObject.FindWithTag("MainCamera").transform;
	}

	private void Update() {
		if (Input.GetButtonDown("Shoot1")) {
			StartCoroutine(Shoot(0));
		}

		if (Input.GetButtonDown("Shoot2")) {
			StartCoroutine(Shoot(1));
		}

		if (Input.GetButtonDown("Shoot3")) {
			StartCoroutine(Shoot(2));
		}

		if (Input.GetButtonDown("Shoot4")) {
			StartCoroutine(Shoot(3));
		}

	}



	private IEnumerator Shoot(int index) {

		if (_camera.localPosition.x >= 0.0f) {

			if (!LeftShootingPoints[index].IsReloading) {
				float power = Random.Range(MinPower, MaxPower);

				CannonSound.PlayOneShot(CannonClip);

				GameObject cannonballClone = Instantiate(CannonBall, LeftShootingPoints[index].Cannon.position, Quaternion.identity);
				cannonballClone.transform.SetParent(_transform);

				Rigidbody CannonballRigidBody = cannonballClone.GetComponent<Rigidbody>();
				CannonballRigidBody.AddForce(LeftShootingPoints[index].Cannon.forward * power);

				LeftShootingPoints[index].IsReloading = true;
				yield return new WaitForSeconds(CannonReloadTime);
				LeftShootingPoints[index].IsReloading = false;
			}
		}
		else {
			if (!RightShootingPoints[index].IsReloading) {
				float power = Random.Range(MinPower, MaxPower);

				CannonSound.PlayOneShot(CannonClip);

				GameObject cannonballClone = Instantiate(CannonBall, RightShootingPoints[index].Cannon.position, Quaternion.identity);
				cannonballClone.transform.SetParent(_transform);

				Rigidbody CannonballRigidBody = cannonballClone.GetComponent<Rigidbody>();
				CannonballRigidBody.AddForce(RightShootingPoints[index].Cannon.forward * power);

				RightShootingPoints[index].IsReloading = true;
				yield return new WaitForSeconds(CannonReloadTime);
				RightShootingPoints[index].IsReloading = false;
			}
		}
		yield return null;
	}

}