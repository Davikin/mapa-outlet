using UnityEngine;
using System.Collections;

public class ButtonDeactivator : MonoBehaviour {
    public void CallButtonsOff() {
        Panel.Instance.ButtonsOff();
        Panel.Instance.CallMoveAgent();
        Panel.Instance.storeClickPoint = false;
        Panel.Instance.ToggleIndicators(false);
        print("storeClickPoint: " + Panel.Instance.storeClickPoint);
    }

    public void CallButtonsOn() {
        Panel.Instance.ButtonsOn();
        Panel.Instance.storeClickPoint = true;
        Panel.Instance.ToggleIndicators(true);
        print("storeClickPoint: " + Panel.Instance.storeClickPoint);
    }

    public void CallActivateButtons() {
        Panel.Instance.ActivateButtons();
    }

    public void CallDeactivateButtons() {
        Panel.Instance.DeactivateButtons();
    }

    public void CallGuideOn() {
        Panel.Instance.ToggleGuideAndCleaner(true);
    }

    public void CallGuideOff() {
        Panel.Instance.ToggleGuideAndCleaner(false);
    }
}
