using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyShoot : MonoBehaviour {
    
    [SerializeField]
    private GameObject Cannonball;
    [SerializeField]
    private List<Transform> ShootingPoints = new List<Transform>();
    [SerializeField]
    private float SecondsBetweenShots = 3.0f;
    [SerializeField]
    private float MinPower = 1000.0f;
    [SerializeField]
    private float MaxPower = 1500.0f;

    private Transform _transform;
    private bool _shouldShoot = false;

    public void StartShooting() {
        _shouldShoot = true;
        StartCoroutine(Shoot());
    }

    public void StopShooting() {
        _shouldShoot = false;
    }

    private void Awake() {
        _transform = transform;
    }

    private IEnumerator Shoot() {
        while (_shouldShoot) {
            foreach (Transform shootingPoint in ShootingPoints) {
                float power = Random.Range(MinPower, MaxPower);

                GameObject cannonballClone = Instantiate(Cannonball, shootingPoint.position, Quaternion.identity);
                cannonballClone.transform.SetParent(_transform);

                Rigidbody CannonballRigidBody = cannonballClone.GetComponent<Rigidbody>();
                CannonballRigidBody.AddForce(shootingPoint.forward * power);

                yield return new WaitForSeconds(SecondsBetweenShots);

            }
            yield return new WaitForSeconds(SecondsBetweenShots);

        }
        yield return null;
    }

}
