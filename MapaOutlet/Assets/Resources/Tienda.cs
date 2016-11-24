using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;


public class Tienda{
    
    [XmlAttribute("nombre")]
    public string nombre;

    [XmlElement("descripcion")]
    public string descripcion;

    [XmlElement("telefono")]
    public string telefono;

    [XmlElement("rutaVideo")]
    public string rutaVideo;
}
