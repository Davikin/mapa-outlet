using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

    //Variables Luis
    public Camera camera3D;
    public Transform pathIndicatorsContainer;
    public GameObject pathIndicator;

    private NavMeshAgent agent;
    private float spawnTime = 1f;
    private float spawnCounter = 0;
    private List<GameObject> pathIndicatorsPool = new List<GameObject>();

    //Variables David /**/
    private Vector3 initialPosition;
    public List<GameObject> activatedIndicators = new List<GameObject>();
    public bool activatingMeshes = false;

    // Use this for initialization
    void Start() {

        agent = this.GetComponent<NavMeshAgent>();

        //Agregamos todos los indicadores a la lista de pool
        foreach (Transform t in pathIndicatorsContainer)
            pathIndicatorsPool.Add(t.gameObject);

        initialPosition = transform.localPosition; /**/
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            MoveAgent();
        }
        if (agent.hasPath) {
            for (int i = 0; i < agent.path.corners.Length - 1; i++) {
                Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.red);
                spawnCounter += Time.deltaTime;
                if (spawnCounter >= spawnTime) {
                    // Buscamos un indicador del pool disponible
                    GameObject indicator = GetIndicator();
                    indicator.transform.position = agent.transform.position;
                    indicator.SetActive(true);
                    // Reseteamos el contador
                    spawnCounter = 0;
                }
            }
        }
    }

    private void MoveAgent() {
        Ray ray = camera3D.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && !Panel.Instance.panel.activeSelf) {
            activatedIndicators.Clear();
            agent.transform.localPosition = initialPosition;
            agent.destination = hit.point;


            // Apagamos todos los indicadores en uso
            foreach (GameObject obj in pathIndicatorsPool) {
                if (obj.activeSelf) {
                    obj.SetActive(false);
                    obj.transform.position = initialPosition;
                }
            }



        }
        else print("No se puede mover el agent porque el panel esta activo");
    }

    private GameObject GetIndicator() {
        foreach (GameObject obj in pathIndicatorsPool) {
            if (!obj.activeSelf) {
                return obj;
            }
        }

        GameObject dummy = (GameObject)Instantiate(pathIndicator, agent.transform.position, pathIndicator.transform.rotation);
        dummy.transform.SetParent(pathIndicatorsContainer);
        pathIndicatorsPool.Add(dummy);
        return dummy;
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "indicadores") {
            //col.gameObject.GetComponent<MeshRenderer>().enabled = true;
            if (!col.gameObject.name.Contains("added")) {
                activatedIndicators.Add(col.gameObject);
                col.gameObject.name += "added";
            }
            if (activatingMeshes) col.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

}