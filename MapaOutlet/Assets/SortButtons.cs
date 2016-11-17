using UnityEngine;
using System.Collections;

public class SortButtons : MonoBehaviour {
    string bname;
    string finalPrint = "";
	// Use this for initialization
	void Start () {
        foreach (Transform button in transform) {
            if (button.name.Contains(".") || button.name.Contains("button")) {
                bname = button.name;
                bname = bname.Substring(bname.IndexOf(".")+1);
                button.name = bname;
            }
        }

        foreach (Transform button in transform) {
            finalPrint += button.name+"\n";
        }

        print(finalPrint);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
