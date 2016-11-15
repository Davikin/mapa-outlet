using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {

    public static Panel Instance { get; private set; }
    public GameObject panel, buttons;

    // Use this for initialization
    void Awake() {
        Instance = this;
    }

    void Update() {
        if (panel == null && GameObject.FindGameObjectWithTag("panel") != null) {
            panel = GameObject.FindGameObjectWithTag("panel");
            panel.SetActive(false);
        }
        if (buttons == null && GameObject.FindGameObjectWithTag("buttons") != null) {
            buttons = GameObject.FindGameObjectWithTag("buttons");
        }
    }

    public void TogglePanel() {
        panel.SetActive(!panel.activeSelf);
        buttons.SetActive(!panel.activeSelf);
    }
}
