using UnityEngine;
using System.Collections;

public class PopManager : MonoBehaviour {

    public static PopManager Instance { get; private set; }
    public GameObject panel;

    // Use this for initialization
    void Awake () {
        Instance = this;
	}
	
    void Update(){
        if (panel == null && GameObject.FindGameObjectWithTag("panel") != null){
            panel = GameObject.FindGameObjectWithTag("panel");
            panel.SetActive(false);
        }
    }

	public void TogglePopUp(int storeIndex, bool showOrHide){
         if(panel != null)
            panel.SetActive(showOrHide);
        //LoadInfo(storeIndex); //get from XML
    }
}
