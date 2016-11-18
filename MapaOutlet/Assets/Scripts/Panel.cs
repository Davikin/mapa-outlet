using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {

    public static Panel Instance { get; private set; }
    public GameObject panel, buttons;
    Agent agent;
    Transform indicadores;
    public Material grey;
    public bool filterIsActive = false;

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
        if (indicadores == null && GameObject.FindGameObjectWithTag("indicadores") != null) {
            indicadores = GameObject.FindGameObjectWithTag("indicadores").transform;
        }

        if (agent == null && GameObject.FindGameObjectWithTag("agent") != null) {
            agent = GameObject.FindGameObjectWithTag("agent").GetComponent<Agent>();
        }
    }

    public void TogglePanel() {
        panel.SetActive(!panel.activeSelf);
        buttons.SetActive(!panel.activeSelf);
    }

    public void PanelOff() {
        if (!panel.activeSelf) return;
        panel.SetActive(false);
        buttons.SetActive(true);
    }
    public void ToggleIndicators(bool onOrOff) {
        //foreach (Transform indicador in indicadores) indicador.GetComponent<SpriteRenderer>().enabled = onOrOff;
        foreach (GameObject indicador in agent.activatedIndicators) indicador.GetComponent<MeshRenderer>().enabled = onOrOff;
        if (!onOrOff) {
            foreach (GameObject indicador in agent.activatedIndicators) indicador.name = indicador.name.Replace("added",""); 
            agent.activatedIndicators.Clear();
        }
        agent.activatingMeshes = onOrOff;
    }
}
