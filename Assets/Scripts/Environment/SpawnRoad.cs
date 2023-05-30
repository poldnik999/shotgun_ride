using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoad : MonoBehaviour
{
    private GameObject roadSection;
    [SerializeField] private float collisionDebug;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(i);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Player" && i == 0)
        {
            roadSection = gameObject.transform.parent.gameObject;
            Debug.Log(roadSection.name);
            i = 1;
            float roadLength = roadSection.GetComponent<BoxCollider>().bounds.size.z;
            Vector3 roadSpawnPoint = new Vector3(roadSection.transform.position.x, roadSection.transform.position.y, roadLength + roadSection.transform.position.z + collisionDebug);
            //Debug.Log(roadSpawnPoint + "   " + roadSection.transform.position);
            GameObject road = Instantiate(roadSection, roadSpawnPoint, Quaternion.identity);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
            Destroy(gameObject);
    }
}
