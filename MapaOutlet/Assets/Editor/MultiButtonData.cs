using UnityEngine;
using UnityEditor;

public class MultiButtonData {

    [MenuItem("Assets/Create/YourClass")]
    public static void CreateAsset() {
        Transform botones = GameObject.Find("Botones").transform;
        string fileName, storeName, phone, local;

        storeName = "2";
        phone = "3";
        local = "Hb-4";

        foreach (Transform child in botones) {
            fileName = child.name;
            fileName = fileName.Substring(fileName.IndexOf(".")+1);
            if(fileName.Contains("+")) fileName = fileName.Remove(fileName.IndexOf("+"));
            storeName = fileName; //asigname el valor antes de que quites lo apostrofes!
            if(fileName.Contains("'")) fileName = fileName.Replace("'","");

            if (child.name.Contains("#")) {
                phone = child.name.Substring(child.name.IndexOf("#") + 1);
                if (phone.Length <= 9) {
                    phone = phone.Insert(3, ".").Insert(6, ".");
                }
                else {
                    phone = phone.Insert(3, ".").Insert(7, ".");
                }
            }
            else {
                phone = "Tel\u00e9fono no disponible";
            }

            if (child.name.Contains("#")) {
                local = child.name.Substring(child.name.IndexOf("+") + 1);
                local = local.Remove(local.IndexOf("#"));
            }
            else {
                local = "N/A";
            }

            ScriptableObjectUtility.CreateAsset<ButtonDataObject>(fileName, storeName, phone, local);
        }
    }
}