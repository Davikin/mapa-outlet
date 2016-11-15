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
    string movieDirectory;
    DirectoryInfo movieDirectoryPath;
    List<string> movieNames = new List<string>();
    bool justStarting = true;
    MediaPlayerCtrl mpc;
    public Texture black;

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

        mpc.m_strFileName = txtr+".mp4";
        mpc.Load(mpc.m_strFileName);
        mpc.Play();
    }

    void Start() {
        movieDirectory = Application.streamingAssetsPath;
        movieDirectoryPath = new DirectoryInfo(movieDirectory);

        fileInfo = movieDirectoryPath.GetFiles("*.mp4", SearchOption.AllDirectories);

        foreach (FileInfo file in fileInfo) {movieNames.Add(file.Name);}

/*        foreach (string name in movieNames) {
            print(name);
        }*/
    }

    void OnDisable() {
        if(movTexture) movTexture.Stop();
        mpc.Stop();
        mpc.UnLoad();
        ri.texture = black;
    }
}