using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class ButtonDataObject : ScriptableObject {

    public string nombreTienda;
    public string numero;
    public string local;
    public string nombreVideo;

    //constructor para cambiar las variables al crear cada instancia
    public void Init(string _nombreTienda, string _numero, string _local) {
        this.nombreTienda = _nombreTienda;
        this.numero = _numero;
        this.local = _local;
    }
}
