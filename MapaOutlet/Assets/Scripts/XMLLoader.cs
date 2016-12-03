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

public class XMLLoader : MonoBehaviour {
    private TextAsset textAsset;
    private string userDirectory;
    //private string userDirectory = "C:/Data/MapaOutlet/";
    private string fileName = "/BaseDeDatos_tiendas.xml";
    bool justStarting = true;
    [SerializeField] Text display;

    TiendaContainer tc;
    ButtonDataObject[] bdos;

    // Use this for initialization
    void Start() {
        userDirectory = Application.persistentDataPath; //The persistent data path is not available 

        print("The persistent data path is: "+ userDirectory);

        //do nothing until previous file finishes being deleted
        while (File.Exists(Application.dataPath + "/Resources/tiendasNew.xml")) File.Delete(Application.dataPath + "/Resources/tiendasNew.xml");

        //print(Application.persistentDataPath);	
        textAsset = Resources.Load("tiendas") as TextAsset;
        print(textAsset.text); //Content before overwriting it with user changes in the outer xml file
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
            print("New input is:\n" + textAsset.text);
            if (display != null) display.text = "New input is:\n" + textAsset.text;
            //Llamar al TiendaLoader
            CallStoreLoader();
        }
        else {
            textAsset = Resources.Load("tiendasNew") as TextAsset;
            print("New input is:\n" + textAsset.text);
            if (display != null) display.text = "New input is:\n" + textAsset.text;
            //Llamar al TiendaLoader
            CallStoreLoader();
        }

        bdos = Resources.LoadAll<ButtonDataObject>("ButtonDataObjects/");

          foreach(ButtonDataObject bdo in bdos) {
              foreach (Tienda tienda in tc.tiendas) {
                  if (bdo.local.Contains(tienda.numLocal)) Compare(bdo,tienda);
              }
          }

        //"Assets/Resources/ButtonDataObjects/ARENA 1.asset" es la ruta! debe ser relativo al project folder

       /* foreach(ButtonDataObject bdo in bdos) {
            print("File name is "+bdo.name);
        } */ //solo para checar el nombre
    }

    private void CallStoreLoader() {
        tc = TiendaContainer.Load("tiendasNew");
        foreach (Tienda tienda in tc.tiendas) {
            print(tienda.nombre + " se encuentra en el local " + tienda.numLocal + ". Su telefono es: " + tienda.phone);
        }
    }

    void Compare(ButtonDataObject _bdo, Tienda _tienda) {
        bool differs = false;
        if (!_bdo.nombreTienda.Contains(_tienda.nombre)) {
            _bdo.nombreTienda = _tienda.nombre;
            if (!_bdo.nombreTienda.Contains("'")) _bdo.nombreVideo = _bdo.nombreTienda.Replace("'","");
            differs = true;
        }

        if (!_bdo.numero.Replace(".","").Contains(_tienda.phone)) {
            _bdo.numero = _tienda.phone.Insert(3,".").Insert(6,".");
            differs = true;
        }

        if (differs) OverwriteBDO(_bdo);
    }

    void OverwriteBDO(ButtonDataObject _bdo) {
        string filePath = "Assets/Resources/ButtonDataObjects/" + _bdo.name;
        File.Copy(filePath + ".asset", filePath + " -copy.asset");
        //AssetDatabase.DeleteAsset(filePath); //no debemos borrar aunque haya
        //AssetDatabase.CreateAsset(_bdo, filePath); //this method DOES overwrite
        //AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();
        // StartCoroutine(WaitForDelete(_bdo, filePath));// wait for file to be deleted
    }

    IEnumerator WaitForDelete(ButtonDataObject _bdo, string _filePath) {
        yield return AssetDatabase.DeleteAsset(_filePath);
        AssetDatabase.CreateAsset(_bdo, _filePath); //this method DOES overwrite
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
