using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TiendaLoader : MonoBehaviour {

    public const string path = "tiendas"; //el path debe coincidir con el nombre del xml
    public GameObject textToShow;

	void Start () {
        TiendaContainer tc = TiendaContainer.Load(path);

        foreach(Tienda tienda in tc.tiendas)
        {
            print(tienda.nombre);
            textToShow.GetComponent<Text>().text += tienda.nombre+"\n";
        }
	}
}
