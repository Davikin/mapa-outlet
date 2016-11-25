using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LocalFilter : MonoBehaviour {
    public Sprite targetSprite, greySprite;
    Transform buttons;
    float filteredOutText = 0.4f;

    void Update() {
        if (buttons == null && GameObject.FindGameObjectWithTag("buttons") != null) buttons = GameObject.FindGameObjectWithTag("buttons").transform;
    }

    public void FilterLocals(bool onOrOff) {
        if (Panel.Instance.filterIsActive) UnfilterLocals();
        foreach (Transform b in buttons) {
            if (b.name.ToLower().Contains("button")) {
                if (onOrOff) {
                    if (b.GetComponent<Image>().sprite != targetSprite) {
                        b.GetComponent<Image>().sprite = greySprite;
                        //b.GetChild(0).GetComponent<Text>().color = new Color(filteredOutText, filteredOutText, filteredOutText);
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
            if (b.name.ToLower().Contains("button")) {
                b.GetComponent<Image>().sprite = targetSprite;
                b.GetChild(0).GetComponent<Text>().color = Color.white;
            }
        }
        Panel.Instance.filterIsActive = false;
    }
}
