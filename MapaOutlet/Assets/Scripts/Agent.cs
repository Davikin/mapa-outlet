using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Agent : MonoBehaviour {

    public Camera camera3D;

    private NavMeshAgent agent;
    private Vector3 initialPosition;
    public List<GameObject> activatedIndicators = new List<GameObject>();
    public List<GameObject> touchedIslands = new List<GameObject>();
    public bool activatingMeshes = false;
    public GameObject destello;
    public bool hideIslands = true;

    // Use this for initialization
    void Start() {
        agent = this.GetComponent<NavMeshAgent>();
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {

        if (EventSystem.current.currentSelectedGameObject != null && !Panel.Instance.panel.activeSelf && Input.GetMouseButtonDown(0) && (EventSystem.current.currentSelectedGameObject.name.ToLower().Contains("button") || EventSystem.current.currentSelectedGameObject.name.ToLower().Contains("bano") || EventSystem.current.currentSelectedGameObject.name.ToLower().Contains("puerta") || EventSystem.current.currentSelectedGameObject.name.ToLower().Contains("info"))) {
            MoveAgent();     
        }
    }

    private void MoveAgent() {
        Ray ray = camera3D.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (agent.hasPath) {
            agent.Stop();
            agent.ResetPath();
        }
        //agent.updatePosition = false;
        agent.enabled = false;
        agent.transform.localPosition = initialPosition;
        //agent.updatePosition = true;
        agent.enabled = true;
        if (Physics.Raycast(ray, out hit) && !Panel.Instance.panel.activeSelf) {
            agent.destination = hit.point;
        }
        else print("No se puede mover el agent porque el panel esta activo. Raycast = "+ Physics.Raycast(ray, out hit)+"PanelIsActive: "+ !Panel.Instance.panel.activeSelf);
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "indicadores") {
            if (!col.gameObject.name.Contains("added")) {
                activatedIndicators.Add(col.gameObject);
                GameObject nuevoDestello = Instantiate(destello);
                nuevoDestello.transform.SetParent(col.transform);
                nuevoDestello.transform.localEulerAngles = Vector3.zero;
                nuevoDestello.transform.localPosition = Vector3.zero;
                col.gameObject.name += "added";
            }
            if (activatingMeshes) col.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            if (col.GetComponent<LinkToIsland>()) {
                touchedIslands.Add(col.gameObject);
                if (hideIslands) col.GetComponent<LinkToIsland>().island.SetActive(false);
            }
        }
    }
}