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

    // Use this for initialization
    void Awake() {
        ri = GetComponent<RawImage>();
    }

    void OnEnable () {
        ChooseMovie();
        ri.material.SetTexture("_MainTex", Resources.Load("Movies/" + txtr) as Texture);
        movTexture = GetComponent<RawImage>().material.mainTexture as MovieTexture;
        if (!justStarting && movTexture) {
            movTexture.Play();
            movTexture.loop = true;
        }
        justStarting = false;
	}

    void Start() {
        movieDirectory = Application.dataPath + "/Resources/Movies";
        movieDirectoryPath = new DirectoryInfo(movieDirectory);

        fileInfo = movieDirectoryPath.GetFiles("*.mp4", SearchOption.AllDirectories);

        foreach (FileInfo file in fileInfo) {
            movieNames.Add(file.Name.Replace(".mp4",""));
        }

        foreach (string name in movieNames) {
            print(name);
        }
    }

    void OnDisable() {
        if(movTexture) movTexture.Stop(); 
    }

    void ChooseMovie() {

        string buttonPressed = "";
        if (EventSystem.current.currentSelectedGameObject) buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        else {
            foreach (string name in movieNames) {
                buttonPressed = name;
                break;
            }
        }

        if (buttonPressed.Contains("-")) {
            int dashIndex = buttonPressed.IndexOf("-");
            if (buttonPressed.Contains("'")) buttonPressed.Replace("'","");
            txtr = buttonPressed.Substring(dashIndex + 1);
        }
    }
}