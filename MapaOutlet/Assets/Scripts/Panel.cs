using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

    private TextAsset textAsset;
    private string userDirectory;
    //private string userDirectory = "C:/Data/MapaOutlet/";
    private string fileName = "/BaseDeDatos_tiendas.xml";
    bool justStarting = true;
    [SerializeField]
    Text display;
    [SerializeField]
    Text debugText;

    public TiendaContainer tc;
    ButtonDataObject[] bdos;

    public Animator guiaColorAnim, mapCleanerAnim;

    public WWW userFile;
    public bool loadDataFromXML;
    // Use this for initialization
    void Awake() {
        Instance = this;
    }

    void Start() {
        panelAnim = panel.GetComponent<Animator>();
        panel.SetActive(false);
        if (!Debug.isDebugBuild) Cursor.visible = false;
        foreach (GameObject island in islands) island.SetActive(showIslands);

        userDirectory = Application.dataPath; //The persistent data path is not available 

        print("El XML editable se encuentra en " + userDirectory);

        //do nothing until previous file finishes being deleted
        while (File.Exists(Application.dataPath + "/Resources/tiendasNew.xml")) File.Delete(Application.dataPath + "/Resources/tiendasNew.xml");

        textAsset = Resources.Load("tiendas") as TextAsset;
        //print(textAsset.text); //Content before overwriting it with user changes in the outer xml file
        if (!Directory.Exists(userDirectory)) { //if the directory with outer xml does not exist...
            Directory.CreateDirectory(userDirectory); //... create it!
        }

        if (!File.Exists(userDirectory + fileName)) { //as well, create the file if it does not exist // NO HAY NECESIDAD DE CHECAR SI YA EXISTE, PORQUE NO HARA EL OVERWRITE CUANDO YA EXISTA... MEJOR DICHO, JAMAS HABRA OVERWRITE
            File.WriteAllText(userDirectory + fileName, textAsset.text); //using the text copied from the file in Resources folder
        }

        File.Copy(userDirectory + fileName, Application.dataPath + "/Resources/tiendasNew.xml", true); //overwrite the xml file in Resources with the one in the outer folder
        //persistentTextAsset = (TextAsset)Resources.Load("C:/Data/MapaOutlet/tiendas.xml");
        while (!File.Exists(Application.dataPath + "/Resources/tiendasNew.xml")) {
            //just wait for the file to be loaded
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh(); //While running in the editor, the file does not get reimported automatically, so I must force it
#endif

        textAsset = Resources.Load("tiendasNew") as TextAsset;

        if (Resources.Load("tiendasNew")) {
            //print("New input is:\n" + textAsset.text);
            if (display != null) display.text = "New input is:\n" + textAsset.text;
            //Llamar al TiendaLoader
            StartCoroutine(CallStoreLoader());
        }
        else {
            textAsset = Resources.Load("tiendasNew") as TextAsset;
            //print("New input is:\n" + textAsset.text);
            if (display != null) display.text = "New input is:\n" + textAsset.text;
            //Llamar al TiendaLoader
            StartCoroutine(CallStoreLoader());
        }

        bdos = Resources.LoadAll<ButtonDataObject>("ButtonDataObjects/");

    }

    public void TogglePanel(ButtonDataObject dataObject) {
        if (panel.activeSelf) {
            StartCoroutine(WaitAnimation());
            return;
        }

        foreach (Transform btn in buttons.transform)
            if (btn.GetComponent<Button>())
                btn.GetComponent<Button>().enabled = !panel.activeSelf;

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
            if (showIslands)
                foreach (GameObject island in agent.touchedIslands) island.GetComponent<LinkToIsland>().island.SetActive(false);
            agent.hideIslands = true;
        }
        agent.activatingMeshes = onOrOff;

    }

    public void ActivateButtons() {
        foreach (Transform btn in buttons.transform)
            if (btn.GetComponent<Button>() != null) btn.GetComponent<Button>().enabled = true;
    }

    public void DeactivateButtons() {
        foreach (Transform btn in buttons.transform)
            if(btn.GetComponent<Button>() != null) btn.GetComponent<Button>().enabled = false;
    }

    public void ButtonsOff() {
        if (buttons.activeSelf) buttons.SetActive(false);
    }

    public void ButtonsOn() {
        if (!buttons.activeSelf) buttons.SetActive(true);
    }
    private IEnumerator CallStoreLoader() {
        //It is MANDATORY to include the file:// piece before the URL to load a WWW
        userFile = new WWW("file://" + Application.dataPath+"/BaseDeDatos_tiendas.xml");
        yield return userFile;
        tc = TiendaContainer.Load(userFile);
    }

    void Compare(ButtonDataObject _bdo, Tienda _tienda) {
        bool differs = false;
        if (_bdo.nombreTienda != _tienda.nombre) {
            _bdo.nombreTienda = _tienda.nombre;
            if (!_bdo.nombreTienda.Contains("'")) _bdo.nombreVideo = _bdo.nombreTienda.Replace("'", "");
            debugText.text += "\tNew video name: " + _bdo.nombreVideo;
            differs = true;
        }

        if (_bdo.numero.Replace(".", "") != _tienda.phone) {
            if (_tienda.phone.Length == 7) _bdo.numero = _tienda.phone.Insert(3, ".").Insert(6, ".");
            debugText.text += "\tNew phone: " + _bdo.numero;
            differs = true;
        }

        if (differs) debugText.text += "\t";
    }

    public void ToggleGuideAndCleaner(bool onOrOff) {
        
        guiaColorAnim.SetTrigger(onOrOff ? "in" : "out");
        mapCleanerAnim.SetTrigger(onOrOff ? "in" : "out");
    }
}
