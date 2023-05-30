using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject generateObjects;
    public GameObject generateFolder;
    public GameObject LevelController;

    public Vector3 objectSize = new Vector3(0f, 0f, 0f);    // Увеличение / уменьшение коллизии обьектов генерации
    public Vector3 sizeArea = new Vector3(0f, 0f, 0f);
    public bool rotation = false;
    public int spawnCount;
    public double spawnChance;
    // Start is called before the first frame update
    void Start()
    {
        if(generateFolder.transform.childCount == 0)
        {
            LevelController = GameObject.Find("LevelControl");
            GenerateObjects param = LevelController.GetComponent<GenerateObjects>();
            param.SpawnArea = gameObject;
            param.GenerateObject = generateObjects;
            param.GenerateFolder = generateFolder;
            param.SpawnCount = spawnCount;
            param.SpawnСhance = spawnChance;
            param.ObjectSize = objectSize;
            param.SizeArea = sizeArea;
            param.rotation = rotation;
            param.Generate();
        }
        
    }
}
