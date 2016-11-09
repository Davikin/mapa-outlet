using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    // Use this for initialization
    void Start() {
        foreach (Transform child in transform){
            if (child.gameObject.name.ToLower().Contains("button") && !child.gameObject.name.ToLower().Contains("poligonal")){
                GameObject buttonText = Instantiate(Resources.Load("buttonText"), child) as GameObject;
                buttonText.transform.SetParent(child.transform);
                RectTransform buttonTextRect = buttonText.GetComponent<RectTransform>();
                buttonTextRect.offsetMin = new Vector2(0f, 0f);
                buttonTextRect.offsetMax = new Vector2(0f, 0f);
                if (child.gameObject.name.Contains("vertical") || child.gameObject.name.Contains("terraza")){
                    Text textComponent = buttonText.GetComponent<Text>();
                    textComponent.resizeTextForBestFit = true;
                    if (!child.gameObject.name.Contains("terraza")){
                        buttonTextRect.eulerAngles = new Vector3(0, 0, 90f);
                    }
                    else{
                        textComponent.resizeTextMaxSize = 27;
                    }
                }
                buttonText.transform.localPosition = new Vector3(buttonText.transform.localPosition.x, buttonText.transform.localPosition.y, 0);
            }
        }
	}

    void Update(){
        
    }
}
