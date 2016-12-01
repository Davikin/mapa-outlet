using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Panel : MonoBehaviour {

    public static Panel Instance { get; private set; }
    public GameObject panel, buttons;
    public PlayMovie play;
    public Agent agent;
    public bool filterIsActive = false;
    public bool showFlashes = false;
    public bool showIslands;
    public GameObject[] islands;

    private Animator panelAnim;

    // Use this for initialization
    void Awake() {
        Instance = this;
    }

    void Start() {
        panelAnim = panel.GetComponent<Animator>();
        panel.SetActive(false);
        if (!Debug.isDebugBuild) Cursor.visible = false;
        foreach (GameObject island in islands) island.SetActive(showIslands);
    }

    public void TogglePanel(ButtonDataObject dataObject) {
        if (panel.activeSelf) {
            StartCoroutine(WaitAnimation());
            return;
        }

        foreach (Transform btn in buttons.transform)
            if(btn.GetComponent<Button>())
                btn.GetComponent<Button>().enabled = !panel.activeSelf;
        //buttons.SetActive(!panel.activeSelf);
        if (showIslands)
            foreach (GameObject island in islands)
                island.SetActive(true);

        panel.SetActive(!panel.activeSelf);

        if (panel.activeSelf) { 
            play.FillData(dataObject);
        }
    }

    public void PanelOff() {
        if (panel.activeSelf) {
            StartCoroutine(WaitAnimation());
        }
    }

    IEnumerator WaitAnimation() {
        if (showIslands)
            foreach (GameObject island in islands)
                island.SetActive(true);
        panelAnim.SetTrigger("panelOut");
        yield return new WaitForSeconds(1f); //suponiendo que el fadeout dure 1s
        panel.SetActive(false);
        //buttons.SetActive(true);
        foreach (Transform btn in buttons.transform)
            if (btn.GetComponent<Button>())
                btn.GetComponent<Button>().enabled = true;
    }

    IEnumerator WaitForToggle() {

        yield return new WaitForEndOfFrame();
    }

    public void ToggleIndicators(bool onOrOff) {
        showFlashes = onOrOff;
        foreach (GameObject indicador in agent.activatedIndicators) indicador.GetComponent<SpriteRenderer>().enabled = onOrOff;
        if (!onOrOff) {
            foreach (GameObject indicador in agent.activatedIndicators)
                indicador.name = indicador.name.Replace("added", "");
            agent.activatedIndicators.Clear();
            agent.hideIslands = false;
            agent.touchedIslands.Clear();
            if (showIslands)
                foreach (GameObject island in islands)
                    island.SetActive(true);
        }
        else {
            if(showIslands)
            foreach (GameObject island in agent.touchedIslands) island.GetComponent<LinkToIsland>().island.SetActive(false);
            agent.hideIslands = true;
        }
        agent.activatingMeshes = onOrOff;
       
    }

    public void ButtonsOff() {
        if (buttons.activeSelf) buttons.SetActive(false);
    }

    public void ButtonsOn() {
        if (!buttons.activeSelf) buttons.SetActive(true);
    }
}
