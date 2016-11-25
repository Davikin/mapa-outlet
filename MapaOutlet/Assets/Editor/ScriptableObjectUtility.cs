using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility {
    /// <summary>
    //	This makes it easy to create, name and place unique new ScriptableObject asset files.
    /// </summary>
    public static void CreateAsset<T>(string fileName, string nombreTienda, string numero, string local) where T : ScriptableObject { //added the name parameter
        T asset = ScriptableObject.CreateInstance<T>();

        ButtonDataObject bdo = asset as ButtonDataObject; //added the assignment and casting

        bdo.Init(nombreTienda,numero,local,fileName);

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "") {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "") {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path+"/" + fileName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}