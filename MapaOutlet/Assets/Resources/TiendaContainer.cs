using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("TiendasGrupo")]
public class TiendaContainer{

    [XmlArray("tiendas")]
    [XmlArrayItem("tienda")]
    public List<Tienda> tiendas = new List<Tienda>();
	
    public static TiendaContainer Load(string path){
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(TiendaContainer));

        StringReader reader = new StringReader(_xml.text);

        TiendaContainer tiendas = serializer.Deserialize(reader) as TiendaContainer;

        reader.Close();

        return tiendas;
    }
}
