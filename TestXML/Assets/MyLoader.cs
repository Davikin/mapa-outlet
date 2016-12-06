using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyLoader : MonoBehaviour {

    public GameObject tcObj;
    TiendaContainer tc;
    public Text debugText;

    // Use this for initialization
    void Start() {
        StartCoroutine(Load());
        
        /* //It won't work here because hasn't loaded yet at this point
        foreach(Tienda tienda in tc.tiendas) {
            print("La tienda es "+tienda.nombre);
        }*/
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator Load() {
        WWW www = new WWW("file://C:/Users/DAVID BECERRA/AppData/LocalLow/Bromio/Mapa Outlet/BaseDeDatos_tiendas.xml");
        yield return www;
        tc = TiendaContainer.Load(www);
        int i = 0;
        foreach (Tienda tienda in tc.tiendas) {
            debugText.text += tienda.nombre + ". Telefono: " + tienda.phone + "\t";
            i++;
            if (i % 3 == 0) debugText.text += "\n";
        }
    }
}
