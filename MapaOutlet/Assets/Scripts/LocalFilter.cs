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
                    if (!b.GetComponent<Image>().sprite.name.ToLower().Contains(targetColor))
                        for (int i = 0; i <= sc.targetSprites.Length - 1; i++) {
                            if (b.GetComponent<Image>().sprite == sc.targetSprites[i])
                                b.GetComponent<Image>().sprite = sc.greySprites[i];
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
            if (b.name.ToLower().Contains("button") || b.name.ToLower().Contains("isla")) {
                for (int i = 0; i <= sc.targetSprites.Length - 1; i++) {
                    if (b.GetComponent<Image>().sprite == sc.greySprites[i])
                        b.GetComponent<Image>().sprite = sc.targetSprites[i];
                }
            }
        }
        Panel.Instance.filterIsActive = false;
    }
}
