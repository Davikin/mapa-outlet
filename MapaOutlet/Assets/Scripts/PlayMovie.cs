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

    public void FillData(ButtonDataObject _dataObj)
    {
        localName = _dataObj.local;
        storeName = _dataObj.nombreTienda;
        phone = _dataObj.numero;
        nombreVideo = _dataObj.nombreVideo; //tambien sera el nombre para los iconos

        logo = null;
        logo = Resources.Load("logos/" + nombreVideo, typeof(Sprite)) as Sprite;
        print(logo);

        nombreVideo = nombreVideo + ".mp4";

        logoImage.enabled = logo != null ? true : false;
        logoText.enabled = logo == null ? true : false;

        if (logo != null) logoImage.sprite = logo;

        logoText.text = storeName;

        storeNameText.text = storeName;
        numeroText.text = _dataObj.numero;
        localText.text = _dataObj.local;
        
        // Corre video
        if (nombreVideo.Length > 0)
        {
            mpc.Load(nombreVideo);
            mpc.Play();
        }

    }

}