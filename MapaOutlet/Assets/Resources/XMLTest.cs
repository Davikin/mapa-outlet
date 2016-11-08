using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

public class XMLTest : MonoBehaviour
{

    public TextAsset xmlFile;

    void Start()
    {
        EveryPlayer everyPlayer = EveryPlayer.Load(xmlFile);
        Debug.Log("Player1's level is " + EveryPlayer.enemy.Level);
    }

}
