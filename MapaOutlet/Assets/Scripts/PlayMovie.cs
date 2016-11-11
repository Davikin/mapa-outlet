using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayMovie : MonoBehaviour {
    public MovieTexture movTexture;
    
	// Use this for initialization
	void OnEnable () {
        movTexture = GetComponent<RawImage>().material.mainTexture as MovieTexture;
        movTexture.Play();
        movTexture.loop = true;
	}
}
