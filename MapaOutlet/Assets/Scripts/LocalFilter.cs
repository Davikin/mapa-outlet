using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LocalFilter : MonoBehaviour {
    public string targetColor;
    public SpriteContainer sc;
    Transform buttons;

    void Update() {
        if (buttons == null && GameObject.FindGameObjectWithTag("buttons") != null) buttons = GameObject.FindGameObjectWithTag("buttons").transform;
    }

    public void FilterLocals(bool onOrOff) {
        if (Panel.Instance.filterIsActive) UnfilterLocals();
        foreach (Transform b in buttons) {
            if (b.name.ToLower().Contains("button") || b.name.ToLower().Contains("isla")) {
                if (onOrOff) {
                    if (!Panel.Instance.loadDataFromXML) {
                        if (!b.GetComponent<Image>().sprite.name.ToLower().Contains(targetColor)) {
                            for (int i = 0; i <= sc.targetSprites.Length - 1; i++) {
                                if (b.GetComponent<Image>().sprite == sc.targetSprites[i])
                                    b.GetComponent<Image>().sprite = sc.greySprites[i];
                            }
                        }
                    }
                    else {
                        string colorSuffix = b.name.Substring(b.name.IndexOf("@")+1);
                        if (!colorSuffix.Contains(targetColor))
                            b.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
                    }
                    Panel.Instance.filterIsActive = true;
                }
                else {
                    UnfilterLocals();
                }
            }
        }
    }

    public void UnfilterLocals() {
        foreach (Transform b in buttons) {
            if (!Panel.Instance.loadDataFromXML) {
                if (b.name.ToLower().Contains("button") || b.name.ToLower().Contains("isla")) {
                    for (int i = 0; i <= sc.targetSprites.Length - 1; i++) {
                        if (b.GetComponent<Image>().sprite == sc.greySprites[i])
                            b.GetComponent<Image>().sprite = sc.targetSprites[i];
                    }
                }
            }
            else {
                string colorSuffix = b.name.Substring(b.name.IndexOf("@") + 1);
                Color colorToAssign = new Color();
                switch (colorSuffix.ToLower()) {
                    case "magenta": colorToAssign = new Color(192f / 255, 54f / 255, 118f / 255); break;
                    case "azul": colorToAssign = new Color(72f / 255, 117f / 255, 178f / 255); break;
                    case "amarillo": colorToAssign = new Color(230f / 255, 192f / 255, 67f / 255); break;
                    case "morado": colorToAssign = new Color(95f / 255, 82f / 255, 151f / 255); break;
                    case "naranja": colorToAssign = new Color(213f / 255, 133f / 255, 36f / 255); break;
                    case "verde": colorToAssign = new Color(118f / 255, 168f / 255, 74f / 255); break;
                    case "turquesa": colorToAssign = new Color(127f / 255, 183f / 255, 181f / 255); break;
                    case "rojo": colorToAssign = new Color(153f / 255, 32f / 255, 29f / 255); break;
                    case "indigo": colorToAssign = new Color(5f / 255, 57f / 255, 134f / 255); break;
                    case "salmon": colorToAssign = new Color(213f/255, 138f / 255, 144f / 255); break;
                }
                if (b.name.ToLower().Contains("button") || b.name.ToLower().Contains("isla")) b.GetComponent<Image>().color = colorToAssign;
            }
        }
        Panel.Instance.filterIsActive = false;
    }
}
