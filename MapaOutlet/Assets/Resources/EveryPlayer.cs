using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class EveryPlayer
{

    public static Enemy enemy;

    public static EveryPlayer Load(TextAsset xmlFile)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EveryPlayer));
        return xmlSerializer.Deserialize(new StringReader(xmlFile.text)) as EveryPlayer;
    }

}

[System.Serializable]
public class Enemy
{

    [XmlAttribute("name")]
    public string Name;

    public int Level;
    public int XP;

}
