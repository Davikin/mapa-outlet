using UnityEngine;
using System.Collections;

public class MyLoader : MonoBehaviour {

    public GameObject tcObj;
    TiendaContainer tc;

    // Use this for initialization
    void Start() {
        StartCoroutine(Load());
        
        foreach(Tienda tienda in tc.tiendas) {
            print("La tienda es "+tienda.nombre);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    IEnumerator Load() {
        WWW www = new WWW("file://C:/Users/DAVID BECERRA/AppData/LocalLow/Bromio/Mapa Outlet/BaseDeDatos_tiendas.xml");
        yield return www;
        tc = TiendaContainer.Load(www);
        print(www.text);
    }
}
