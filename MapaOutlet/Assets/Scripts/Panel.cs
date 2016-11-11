using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {

    public static Panel Instance { get; private set; }
    public GameObject panel;

    // Use this for initialization
    void Awake() {
        Instance = this;
    }

    void Update() {
        if (panel == null && GameObject.FindGameObjectWithTag("panel") != null) {
            panel = GameObject.FindGameObjectWithTag("panel");
            panel.SetActive(false);
        }
    }

    public void TogglePanel(int infoIndex) {
        panel.SetActive(!panel.activeSelf);
    }
}
