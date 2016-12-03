using UnityEngine;
using System.Collections;

public class ButtonDeactivator : MonoBehaviour {
    public void CallButtonsOff() {
        Panel.Instance.ButtonsOff();
    }

    public void CallButtonsOn() {
        Panel.Instance.ButtonsOn();
    }
}
