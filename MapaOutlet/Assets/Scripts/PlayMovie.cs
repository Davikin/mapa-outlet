using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;

public class PlayMovie : MonoBehaviour {
    public MovieTexture movTexture;
    [SerializeField]
    string txtr = "";
    RawImage ri;
    FileInfo[] fileInfo;
    string movieDirectory, storeName, localName, phone;
    DirectoryInfo movieDirectoryPath;
    List<string> movieNames = new List<string>();
    bool justStarting = true;
    MediaPlayerCtrl mpc;
    public Texture black;
    Text logoText, localText;

    // Use this for initialization
    void Awake() {
        ri = GetComponent<RawImage>();
        ri.texture = black;
        mpc = GameObject.FindGameObjectWithTag("vidManager").GetComponent<MediaPlayerCtrl>();
    }

    void OnEnable () {
        string buttonPressed = "";
        if (EventSystem.current.currentSelectedGameObject) buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        else {
            foreach (string name in movieNames) {
                buttonPressed = name;
                break;
            }
        }

        if (buttonPressed.Contains(".")) {
            int dashIndex = buttonPressed.IndexOf(".");
            if (buttonPressed.Contains("'")) buttonPressed.Replace("'", "");
            txtr = buttonPressed.Substring(dashIndex + 1);
        }

        storeName = txtr;

        if (txtr.Contains("+")) {
           localName = "LOCAL "+storeName.Substring(txtr.IndexOf("+")+1);
           storeName = txtr.Remove(txtr.IndexOf("+"));
        }
        else {
            localName = "S/N";
        }

        if (localName.Contains("#")) {
            localName = localName.Remove(localName.IndexOf("#"));
        }

        if (txtr.Contains("'")) {
            txtr = txtr.Replace("'","");
        }

        if (txtr.Contains("#")) {
            phone = txtr.Substring(txtr.IndexOf("#") + 1);
            phone = phone.Insert(3,".");
            phone = phone.Insert(6, ".");
            phone = "Tel. " + phone;
            print(phone);
            txtr = txtr.Remove(txtr.IndexOf("#"));
        }
        else {
            phone = "Tel\u00e9fono no disponible";
        }

        txtr = storeName;
        if (txtr.Contains("'")) txtr = txtr.Replace("'","");

        mpc.m_strFileName = txtr+".mp4";
        mpc.Load(mpc.m_strFileName);
        mpc.Play();

        foreach(Transform sibling in transform.parent) {
            if (sibling.name == "nombre") {
                sibling.gameObject.GetComponent<Text>().text = storeName;
            }
            if (sibling.name == "telefono") {
                sibling.gameObject.GetComponent<Text>().text = phone;
            }
            if (sibling.name == "local" ) {
                localText = sibling.gameObject.GetComponent<Text>();
                localText.text = localName;
                break;
            }
        }

        logoText = GameObject.FindGameObjectWithTag("logo").GetComponent<Text>();
        logoText.text = storeName;
    }

    void Start() {
        movieDirectory = Application.streamingAssetsPath;
        movieDirectoryPath = new DirectoryInfo(movieDirectory);

        fileInfo = movieDirectoryPath.GetFiles("*.mp4", SearchOption.AllDirectories);

        foreach (FileInfo file in fileInfo) {movieNames.Add(file.Name);}
    }

    void OnDisable() {
        if(movTexture) movTexture.Stop();
        mpc.Stop();
        mpc.UnLoad();
        ri.texture = black;
    }
}