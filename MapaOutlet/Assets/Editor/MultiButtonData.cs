using UnityEngine;
using UnityEditor;

public class MultiButtonData {
    
    [MenuItem("Assets/Create/YourClass")]
    public static void CreateAsset() {
        Transform botones = GameObject.Find("Botones").transform;
        string storeName;

        foreach (Transform child in botones) {
            storeName = child.name;
            storeName = storeName.Substring(storeName.IndexOf(".")+1);
            if(storeName.Contains("+")) storeName = storeName.Remove(storeName.IndexOf("+"));
            if(storeName.Contains("'")) storeName = storeName.Replace("'","");
            ScriptableObjectUtility.CreateAsset<ButtonDataObject>(storeName);
        }
    }
}