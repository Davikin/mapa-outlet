using UnityEngine;
using System.Collections;

public class ButtonDeactivator : MonoBehaviour {
    public void CallButtonsOff() {
        Panel.Instance.ButtonsOff();
    }

    public void CallButtonsOn() {
        Panel.Instance.ButtonsOn();
    }

    public void CallGuideOn() {
        Panel.Instance.ToggleGuideAndCleaner(true);
    }

    public void CallGuideOff() {
        Panel.Instance.ToggleGuideAndCleaner(false);
    }
}
