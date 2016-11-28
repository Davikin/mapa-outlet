using UnityEngine;
using System.Collections;

public class Destello : MonoBehaviour {

    void Start() {
        StartCoroutine(Disappear());
    }

	IEnumerator Disappear() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
