using UnityEngine;
using System.Collections;

public class GetXMLPath : MonoBehaviour {
  

	public void GetPath() {
        Application.OpenURL(Application.persistentDataPath);
    }
}
