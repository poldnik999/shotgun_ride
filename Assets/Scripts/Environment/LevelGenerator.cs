using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject roadSection;
    public float moveSpeed;
    public float collisionDebug;

    private Rigidbody _rb;
    private Rigidbody _rb1;
    private Vector3 movement;
    private GameObject passedRoad;
    public GameObject newRoad;
    // Update is called once per frame
    void Start()
    {
        movement = new Vector3(0f, 0f, -10f);
        newRoad.transform.AddComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //_rb = roadSection.transform.GetComponent<Rigidbody>();
        //_rb.velocity = new Vector3(0, 0, -5);

        //_rb1 = newRoad.transform.GetComponent<Rigidbody>();
        //_rb1.velocity = new Vector3(0, 0, -5);
        //_rb.AddForce(movement * moveSpeed);
        movement = new Vector3(0f, 0f, -10f);
    }
    private int i = 0;
    void Update()
    {
        float roadLength = roadSection.GetComponent<BoxCollider>().bounds.size.z;
        Vector3 roadSpawnPoint = new Vector3(roadSection.transform.position.x, roadSection.transform.position.y, roadLength/2 + collisionDebug);
        
        if ((int)roadSection.transform.position.z == -30)
        {
            Debug.Log(roadSpawnPoint + "   " + roadSection.transform.position);
            passedRoad = roadSection;
            newRoad = Instantiate(roadSection, roadSpawnPoint, Quaternion.identity);
            roadSection = newRoad;

            i++;
        }
        if((int)roadSection.transform.position.z == -60)
            Destroy(roadSection);
        //roadSection.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
        //roadSectWidth = roadSection2.transform.localScale.z;
        //roadSectBound_1 = roadSection1.GetComponent<BoxCollider>().bounds.max.z;
        //roadSectBound_2 = roadSection2.GetComponent<BoxCollider>().bounds.max.z;
        //roadSection1.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
        //roadSection2.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
        //if (roadSectBound_1 <= 30)
        //{
        //    isFirstRoadPass = true;
        //    roadSection2.transform.position = new Vector3(0f, 0f, roadSectBound_1);
        //}
        ////roadSection2.transform.position = new Vector3(0f, 0f, roadSectBound_1 + 3.5f);
        //if (isFirstRoadPass && roadSectBound_2 <= 30)
        //{
        //    roadSection1.transform.position = new Vector3(0f, 0f, roadSectBound_2);
        //}
        //Debug.Log(roadSectBound_1+"  "+ roadSectBound_2);
        //roadSection1.GetComponent<BoxCollider>().bounds.max.z += 30;
        //if(roadSectBound_2 < 30)
        //{
        //    roadSection1.transform.position = new Vector3(0f, 0f, roadSectBound_2 + 3.5f);
        //}
        //else if (roadSection2.transform.position.z < roadSectWidth)
        //{
        //    roadSection2.transform.position = new Vector3(0f, 0f, roadSectWidth);
        //}
        //if (roadSection2.transform.position.z < roadSectWidth)
        //{
        //    roadSection2.transform.position = new Vector3(0f, 0f, roadSectWidth);
        //}
        //if (makeSection == false)
        //{
        //    makeSection = true;
        //    //StartCoroutine(GenerateSection());
        //}
        //IEnumerator GenerateSection()
        //{

        //    if (roadSection1.transform.position.z == -63f)
        //    {
        //       roadSection1.transform.position = new Vector3(0f, 0f, zPos);
        //    }
        //    //roadSection2 = Instantiate(roadSection1, new Vector3(0, 0, zPos), Quaternion.identity);
        //    zPos += 63;
        //    yield return new WaitForSeconds(6);
        //    makeSection = false;
        //    //roadSection2.transform.position = new Vector3(0f, 0f, zPos);
        //}
    }
}
