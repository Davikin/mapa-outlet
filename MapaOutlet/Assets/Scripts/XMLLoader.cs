using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class XMLLoader : MonoBehaviour {
    TextAsset textAsset;
    XmlDocument xmldoc;

    void Start() {
        textAsset = (TextAsset)Resources.Load("tiendas");
        xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);
    }

}
