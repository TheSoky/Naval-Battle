using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestruct : MonoBehaviour {

    [SerializeField]
    private float Timer = 2.0f;

    private void Awake() {
        Invoke("DestroyNow", Timer);
    }

    private void DestroyNow() {
        Destroy(gameObject);
    }

}
