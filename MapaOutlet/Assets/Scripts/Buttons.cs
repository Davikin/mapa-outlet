using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    public string storeName;
    bool dataIsUpdatedFromXML = false;
    Text localText, numeroText, storeNameText, textComponent;
    public SpriteContainer sc; //REQUIRED FOR THE XML-DEPENDENT COLORING FOR THE LOCALS

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
                        textComponent = buttonText.GetComponent<Text>();
                        if(!child.gameObject.name.Contains("diagonal"))
                            textComponent.resizeTextForBestFit = true;
                        if (!child.gameObject.name.ToLower().Contains("terraza")) {
                            buttonTextRect.localEulerAngles = new Vector3(0, 0, 90f);
                        }
                        else {
                            textComponent.resizeTextMaxSize = 27;
                        }
                        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
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
                    buttonTextRect.offsetMin = new Vector2(0f, -20f);
                    buttonTextRect.offsetMax = new Vector2(0f, -20f);
                }

                if (child.gameObject.name.Contains("up")) {
                    buttonTextRect.offsetMin = new Vector2(0f, 13.6f);
                    buttonTextRect.offsetMax = new Vector2(0f, 13.6f); ;
                }
            }
            if (child.gameObject.name.Contains(".")) {
                int dashIndex = child.gameObject.name.IndexOf(".");
                storeName = child.gameObject.name.Substring(dashIndex + 1);
                if (storeName.Contains(" 01")) storeName = storeName.Replace(" 01","");
                if (storeName.Contains(" 02")) storeName = storeName.Replace(" 02", "");
                if (storeName.IndexOf("+") > 0) storeName = storeName.Remove(storeName.IndexOf("+"));
                if (storeName.ToLower().Contains("hampton") || storeName.ToLower().Contains("plate")) storeName = storeName.Replace(" ","\n");
                if (storeName.Contains(" ") && (child.name.Contains("isla") || child.name.ToLower().Contains("vertical"))) {
                    buttonText.GetComponent<Text>().text = storeName.Replace(" ", "\n");
                    if (storeName.ToUpper().Contains("CUEVA DEL ZORRO")) {
                        storeName = "LA CUEVA\nDEL ZORRO";
                        buttonText.GetComponent<Text>().text = storeName;
                    }
                    if (storeName.ToUpper().Contains("FONDA STA CLARA")) {
                        storeName = "LA FONDA\nSTA CLARA";
                        buttonText.GetComponent<Text>().text = storeName;
                    }
                }
                else buttonText.GetComponent<Text>().text = storeName;
            }

            if (storeName.ToLower().Contains("zwill")){
                buttonText.GetComponent<Text>().fontSize = 5;
            }
        }
	}

    void Update(){ //Este bloque Update se asegura de que los colores de todos los locales se actualicen de acuerdo a los datos del XML
        if (!Panel.Instance.loadDataFromXML) return; //¿Elegi NO cargar el XML? No hacer nada de lo que sigue en el Update
        
        if(Panel.Instance.tc != null) //¿Ya está cargado el XML? Esto es necesario porque el XML no se carga instantaneamente
            if (!dataIsUpdatedFromXML) { //¿Ya se realizó la carga del XML?
                string localNumber = "";
                foreach (Transform child in transform) {
                    string colorSuffix = "";
                    Color colorToAssign = new Color(0,0,0);
                    Image currentImage = child.GetComponent<Image>();
                    if (child.name.IndexOf("+") > 0 || child.name.Contains("isla")) {
                        localNumber = child.name.Substring(child.name.IndexOf("+")+1);
                        if (localNumber.Contains("#")) localNumber = localNumber.Remove(localNumber.IndexOf("#"));
                        foreach (Tienda tienda in Panel.Instance.tc.tiendas) {
                            if (tienda.numLocal == localNumber) {
                                textComponent = child.GetChild(0).GetComponent<Text>();
                                textComponent.text = tienda.nombre;

                                //DONT KNOW IF THIS IS REQUIRED, BUT IS A GREAT FEATURE: COLOR EACH LOCAL ACCORDING TO ITS CATEGORY IN XML
                                
                                if (!child.name.ToLower().Contains("poligon")) currentImage.sprite = sc.whiteSprite;
                                colorToAssign = new Color(0, 0, 0);
                                colorSuffix = "";
                                switch (tienda.category.ToUpper()) {
                                    case "DAMA": colorToAssign = new Color(192f / 255, 54f / 255, 118f / 255); colorSuffix = "magenta"; break;
                                    case "CABALLERO": colorToAssign = new Color(72f / 255, 117f / 255, 178f / 255); colorSuffix = "azul"; break;
                                    case "AMBOS": colorToAssign = new Color(230f / 255, 192f / 255, 67f / 255); colorSuffix = "amarillo"; break;
                                    case "NIÑOS": colorToAssign = new Color(95f / 255, 82f / 255, 151f / 255); colorSuffix = "morado"; break;
                                    case "DEPORTES": colorToAssign = new Color(213f / 255, 133f / 255, 36f / 255); colorSuffix = "naranja"; break;
                                    case "ZAPATOS": colorToAssign = new Color(118f / 255, 168f / 255, 74f / 255); colorSuffix = "verde"; break;
                                    case "ACCESORIOS": colorToAssign = new Color(127f / 255, 183f / 255, 181f / 255); colorSuffix = "turquesa"; break;
                                    case "COMIDA": colorToAssign = new Color(153f / 255, 32f / 255, 29f / 255); colorSuffix = "rojo"; break;
                                    case "OTROS": colorToAssign = new Color(5f / 255, 57f / 255, 134f / 255); colorSuffix = "indigo"; break;
                                }
                            }
                        }
                        if (colorSuffix != "") {
                            currentImage.color = colorToAssign;
                            child.name += "@" + colorSuffix;
                        }
                    }
                    if (child.name.Contains("isla") && colorSuffix == "") {
                        colorToAssign = new Color(213f / 255, 138f / 255, 144f / 255);
                        colorSuffix = "salmon";
                        currentImage.color = colorToAssign;
                        child.name += "@" + colorSuffix;
                    }
                }
                dataIsUpdatedFromXML = true; //La carga del XML ya se realizó, lo ponemos en true para evitar que la carga se repita
            }
    }
}
