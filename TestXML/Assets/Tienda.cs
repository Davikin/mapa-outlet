using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Tienda {
    [XmlAttribute("nombre")]
    public string nombre;

    [XmlAttribute("local")]
    public string numLocal;

    [XmlElement("telefono")]
    public string phone;
}
