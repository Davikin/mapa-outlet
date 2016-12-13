using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class PlayMovie : MonoBehaviour {
    RawImage ri;
    FileInfo[] fileInfo;
    string movieDirectory, storeName, localName, phone;
    string nombreVideo;
    DirectoryInfo movieDirectoryPath;
    List<string> movieNames = new List<string>();
    bool justStarting = true;
    public MediaPlayerCtrl mpc;
    public Texture black;
    //Text logoText, localText;

    private GameObject panelVideo;
    private Text logoText;
    private Text storeNameText;
    private Text localText;
    private Text numeroText;
    private Image logoImage;

    private string logoDir;
    private DirectoryInfo logoDirInfo;
    private FileInfo[] logoFilesInfo;
    private Sprite logo;

    void Awake() {
        ri = GetComponent<RawImage>();
        ri.texture = black;
        panelVideo = this.transform.parent.gameObject;

        // Referenciamos los Text a lo hijos
        // Hijo 5
        localText = transform.parent.transform.GetChild(5).GetComponent<Text>();
        // Hijo 7
        logoImage = transform.parent.transform.GetChild(7).GetComponent<Image>();
        logoText = transform.parent.transform.GetChild(7).GetChild(0).GetComponent<Text>();
        // Hijo 3
        storeNameText = transform.parent.transform.GetChild(3).GetComponent<Text>();
        // Hijo 4
        numeroText = transform.parent.transform.GetChild(4).GetComponent<Text>();
    }

    void OnDisable() {
        mpc.Stop();
        mpc.UnLoad();
        ri.texture = black;
    }

    public void FillData(ButtonDataObject _dataObj){
        localName = _dataObj.local;
        phone = "";

        /*foreach(Tienda tienda in Panel.Instance.tc.tiendas) {
            print(tienda.numLocal);
        }*/

        /* //THIS BLOCK WILL BE USED WHEN WWW XML FINALLY LOADS CORRECTLY
        foreach (Tienda tienda in Panel.Instance.tc.tiendas) {
            if(localName.Contains(tienda.numLocal)) {
                print("Found the store!");
                storeName = tienda.nombre;
                phone = tienda.nombre;
                nombreVideo = storeName;
                if (storeName.Contains("'")) nombreVideo = nombreVideo.Replace("'","");
            }
        }
        */

        if (Panel.Instance.loadDataFromXML && Panel.Instance.tc != null) {
            print("Xml is ready to load!");
            string searchedLocal = _dataObj.local.Replace("LOCAL ","");
            foreach (Tienda tienda in Panel.Instance.tc.tiendas) {
                if(tienda.numLocal == searchedLocal) { //We won't use the .Contains() statement because if we search A-4, we can match A-4A, scrambling info between locals with similar starts
                    print("Found the local in XML");
                    storeName = tienda.nombre;

                    if (tienda.phone.Length == 7) phone = tienda.phone.Insert(3, ".").Insert(6, ".");
                    else if (tienda.phone.Length > 7) {
                        if (tienda.phone.Contains(",")) {
                            string[] phones = phone.Split(new char[] { ',' });
                            for(int i = 0; i <= phones.Length-1; i++) {
                                if(phones[i].Length == 7) phone += phones[i].Insert(3, ".").Insert(6, ".");
                                else if(phones[i].Length > 7) phone += phones[i].Insert(3, ".").Insert(7, ".");
                                if (i < phones.Length - 1) phone += ",";
                            }
                        }
                        else phone = tienda.phone.Insert(3, ".").Insert(7, ".");
                    }
                    else phone = "Tel\u00e9fono no disponible";

                    nombreVideo = tienda.nombre;
                    if (nombreVideo.Contains("'")) nombreVideo = nombreVideo.Replace("'", "");
                    break;
                }
                else {
                    print("Did not find the local in XML");
                    storeName = _dataObj.nombreTienda;
                    phone = _dataObj.numero;
                    nombreVideo = _dataObj.nombreVideo;
                }
            }
        }
        else {
            print("Did not find the local in XML");
            storeName = _dataObj.nombreTienda;
            phone = _dataObj.numero;
            nombreVideo = _dataObj.nombreVideo;
        }

        logo = null;
        logo = Resources.Load("logos/" + nombreVideo, typeof(Sprite)) as Sprite;
        nombreVideo = nombreVideo + ".mp4";

        logoImage.enabled = logo != null ? true : false;
        logoText.enabled = logo == null ? true : false;

        if (logo != null) logoImage.sprite = logo;

        logoText.text = storeName;

        storeNameText.text = storeName;
        numeroText.text = phone;
        localText.text = _dataObj.local;
        
        // Corre video
        if (nombreVideo.Length > 0){
            mpc.Load(nombreVideo);
            mpc.Play();
        }
    }

}