using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("TiendasGrupo")]
public class TiendaContainer{
    [XmlArray("tiendas")]
    [XmlArrayItem("tienda")]
    public List<Tienda> tiendas = new List<Tienda>();

    public static TiendaContainer Load(string path) {
        TextAsset _xml = Resources.Load(path) as TextAsset; //obtener el textAsset con el metodo que acabo de desarollar en vez de esto
        XmlSerializer serializer = new XmlSerializer(typeof(TiendaContainer));
        StringReader reader = new StringReader(_xml.text);
        TiendaContainer tiendas = serializer.Deserialize(reader) as TiendaContainer;
        reader.Close();
        return tiendas;
    }
}
