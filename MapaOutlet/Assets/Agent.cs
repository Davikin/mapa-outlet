using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour {

    public Camera camera3D;
    public Transform pathIndicatorsContainer;
    public GameObject pathIndicator;

    private NavMeshAgent agent;
    private float spawnTime = 1f;
    private float spawnCounter = 0;
    private List<GameObject> pathIndicatorsPool = new List<GameObject>();

    //private List<GameObject> pathIndicators = new List<GameObject>();

    // Use this for initialization
    void Start() {

        agent = this.GetComponent<NavMeshAgent>();

        //Agregamos todos los indicadores a la lista de pool
        foreach (Transform t in pathIndicatorsContainer)
            pathIndicatorsPool.Add(t.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            MoveAgent();
        }
        if (agent.hasPath)
        {
            for (int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.red);
                spawnCounter += Time.deltaTime;
                if (spawnCounter >= spawnTime)
                {
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
 
    private void MoveAgent()
    {
        Ray ray = camera3D.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            agent.transform.localPosition = Vector3.zero;
            agent.destination = hit.point;
            //agent.SetDestination(hit.point);

            // Apagamos todos los indicadores en uso
            foreach(GameObject obj in pathIndicatorsPool)
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                    obj.transform.position = Vector3.zero;
                }
            }
            

            
        }

    }

    private GameObject GetIndicator()
    {
        foreach (GameObject obj in pathIndicatorsPool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }

        GameObject dummy = (GameObject)Instantiate(pathIndicator, agent.transform.position, pathIndicator.transform.rotation);
        dummy.transform.SetParent(pathIndicatorsContainer);
        pathIndicatorsPool.Add(dummy);
        return dummy;
    }


}
