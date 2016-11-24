using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {

    public static Panel Instance { get; private set; }
    public GameObject panel, buttons;
    public PlayMovie play;
    public Agent agent;
    Transform indicadores;
    public Material grey;
    public bool filterIsActive = false;

    // Use this for initialization
    void Awake() {
        Instance = this;
    }

    void Start()
    {
        panel.SetActive(false);
    }

    public void TogglePanel(ButtonDataObject dataObject ) {
        panel.SetActive(!panel.activeSelf);
        buttons.SetActive(!panel.activeSelf);

        if (panel.activeSelf)
            play.FillData(dataObject);
            
    }

    public void PanelOff() {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            buttons.SetActive(true);
        }
    }
    public void ToggleIndicators(bool onOrOff) {
        foreach (GameObject indicador in agent.activatedIndicators) indicador.GetComponent<SpriteRenderer>().enabled = onOrOff;
        if (!onOrOff) {
            foreach (GameObject indicador in agent.activatedIndicators)
                indicador.name = indicador.name.Replace("added", "");
           agent.activatedIndicators.Clear();
        }
        agent.activatingMeshes = onOrOff;
    }

    IEnumerator WaitForToggle() {

        yield return new WaitForEndOfFrame();
    }
}
