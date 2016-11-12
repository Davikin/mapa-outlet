﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    public string storeName;
    public bool XmlIsEnabled;

    // Use this for initialization
    void Start() {
        GameObject buttonText = new GameObject();
        foreach (Transform child in transform){

            RectTransform buttonTextRect;
            if (child.gameObject.name.ToLower().Contains("button")){
                if (child.gameObject.name.ToLower().Contains("poligonal")) {
                    buttonText = child.GetChild(0).gameObject;
                }
                else {
                    if(child.gameObject.name.Contains("diagonal")) buttonText = Instantiate(Resources.Load("diagonalButtonText"), child) as GameObject;
                    else buttonText = Instantiate(Resources.Load("buttonText"), child) as GameObject;
                    buttonText.transform.SetParent(child.transform);
                    buttonTextRect = buttonText.GetComponent<RectTransform>();
                    buttonTextRect.offsetMin = new Vector2(0f, 0f);
                    buttonTextRect.offsetMax = new Vector2(0f, 0f);
                    if (child.gameObject.name.Contains("vertical") || child.gameObject.name.ToLower().Contains("terraza")) {
                        Text textComponent = buttonText.GetComponent<Text>();
                        if(!child.gameObject.name.Contains("diagonal"))
                            textComponent.resizeTextForBestFit = true;
                        if (!child.gameObject.name.ToLower().Contains("terraza")) {
                            buttonTextRect.localEulerAngles = new Vector3(0, 0, 90f);
                        }
                        else {
                            textComponent.resizeTextMaxSize = 27;
                        }
                    }
                    else if (!child.gameObject.name.Contains("vertical")) {
                        buttonTextRect.eulerAngles = new Vector3(0, 0, 0);
                    }
                    buttonText.transform.localPosition = new Vector3(buttonText.transform.localPosition.x, buttonText.transform.localPosition.y, 0);
                }
            }
            if (child.gameObject.name.ToLower().Contains("isla")) {
                buttonText = Instantiate(Resources.Load("islaText"), child) as GameObject;
                buttonTextRect = buttonText.GetComponent<RectTransform>();

                if (child.gameObject.name.Contains("horizontal")) {
                    buttonTextRect.offsetMin = new Vector2(20f, 0);
                    buttonTextRect.offsetMax = new Vector2(20f, 0);
                    buttonText.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
                }
                else {
                    buttonTextRect.offsetMin = new Vector2(0f, -13.6f);
                    buttonTextRect.offsetMax = new Vector2(0f, -13.6f);
                }

                if (child.gameObject.name.Contains("up")) {
                    buttonTextRect.offsetMin = new Vector2(0f, 13.6f);
                    buttonTextRect.offsetMax = new Vector2(0f, 13.6f); ;
                }
            }
            if (child.gameObject.name.Contains("-") && !XmlIsEnabled) {
                int dashIndex = child.gameObject.name.IndexOf("-");
                storeName = child.gameObject.name.Substring(dashIndex + 1);
                if (storeName.Contains(" ") && child.name.Contains("isla")) buttonText.GetComponent<Text>().text = storeName.Replace(" ", "\n");
                else buttonText.GetComponent<Text>().text = storeName;
            }
            if (storeName.ToLower().Contains("zwill")){
                buttonText.GetComponent<Text>().fontSize = 5;
            }
        }
	}

    void Update(){
        
    }
}
