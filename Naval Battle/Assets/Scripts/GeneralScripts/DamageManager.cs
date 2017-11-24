using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {

    [SerializeField]
    private int Damage = 5;

    private void OnCollisionEnter(Collision other)
    {
        EnemyHealthManager enemyHealthManager = other.gameObject.GetComponent<EnemyHealthManager>();
		PlayerHealthManager playerHealthManager = other.gameObject.GetComponent<PlayerHealthManager>();

		if(playerHealthManager != null) {
			playerHealthManager.AddDamage(Damage);
		}

		if (enemyHealthManager != null) {
			enemyHealthManager.AddDamage(Damage);
		}
	}

}
