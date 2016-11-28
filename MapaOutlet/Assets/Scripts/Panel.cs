using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {

    public static Panel Instance { get; private set; }
    public GameObject panel, buttons;
    public PlayMovie play;
    public Agent agent;
    public bool filterIsActive = false;
    public bool showFlashes = false;

    private Animator panelAnim;

    // Use this for initialization
    void Awake() {
        Instance = this;
    }

    void Start() {
        panelAnim = panel.GetComponent<Animator>();
        panel.SetActive(false);
        if (!Debug.isDebugBuild) Cursor.visible = false;
    }

    public void TogglePanel(ButtonDataObject dataObject) {
        if (panel.activeSelf) {
            StartCoroutine(WaitAnimation());
            return;
        }

        panel.SetActive(!panel.activeSelf);
        buttons.SetActive(!panel.activeSelf);

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
        panelAnim.SetTrigger("panelOut");
        yield return new WaitForSeconds(1f); //suponiendo que el fadeout dure 1s
        panel.SetActive(false);
        buttons.SetActive(true);
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
        }
        agent.activatingMeshes = onOrOff;
    }
}
