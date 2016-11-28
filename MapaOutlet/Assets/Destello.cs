using UnityEngine;
using System.Collections;

public class Destello : MonoBehaviour {

    public AnimationClip anim;

    void Start() {
        if (Panel.Instance.panel.activeSelf || !Panel.Instance.showFlashes) Destroy(gameObject);
       // anim = GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        StartCoroutine(Disappear());
    }

	IEnumerator Disappear() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
