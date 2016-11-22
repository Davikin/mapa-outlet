using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class XMLLoader : MonoBehaviour {
    private TextAsset textAsset;
    private string userDirectory;
    //private string userDirectory = "C:/Data/MapaOutlet/";
    private string fileName = "/BaseDeDatos_tiendas.xml";
    bool justStarting = true;
    [SerializeField] Text display;

    // Use this for initialization
    void Start() {
        userDirectory = Application.persistentDataPath;

        print("The persistent data path is"+ userDirectory);

        //do nothing until file finishes being deleted
        while (File.Exists(Application.dataPath + "/Resources/tiendasNew.xml")) File.Delete(Application.dataPath + "/Resources/tiendasNew.xml");

        //print(Application.persistentDataPath);	
        textAsset = Resources.Load("tiendas") as TextAsset;
        print(textAsset.text); //Content before overwriting it with user changes in the outer xml file
        if (!Directory.Exists(userDirectory)) { //if the directory with outer xml does not exist...
            Directory.CreateDirectory(userDirectory); //... create it!
        }

        if (!File.Exists(userDirectory + fileName)) { //create the file as well if it does not exist // NO HAY NECESIDAD DE CHECAR SI YA EXISTE, PORQUE NO HARA EL OVERWRITE CUANDO YA EXISTA... MEJOR DICHO, JAMAS HABRA OVERWRITE
            File.WriteAllText(userDirectory + fileName, "<!--EN ESTE ARCHIVO USTED PUEDE CAMBIAR LOS NOMBRES DE LAS TIENDAS JUNTO CON SU INFORMACION DE CONTACTO-->\n"+textAsset.text); //using the text copied from the file in Resources folder
        }

        File.Copy(userDirectory + fileName, Application.dataPath + "/Resources/tiendasNew.xml", true); //overwrite the xml file in Resources with the one in the outer folder
        //persistentTextAsset = (TextAsset)Resources.Load("C:/Data/MapaOutlet/tiendas.xml");
        while (!File.Exists(Application.dataPath + "/Resources/tiendasNew.xml")) {
          //just wait for the file to be loaded
        }

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

        textAsset = Resources.Load("tiendasNew") as TextAsset;

        if (Resources.Load("tiendasNew")) {
            print("New input is:\n" + textAsset.text);
            if (display != null) display.text = "New input is:\n" + textAsset.text;
        }
        else {
            textAsset = Resources.Load("tiendasNew") as TextAsset;
            print("New input is:\n" + textAsset.text);
            if (display != null) display.text = "New input is:\n" + textAsset.text;
        }
    }
}
