using System.Collections;
using Random = System.Random;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class GenerateObjects : MonoBehaviour
{
    private GameObject spawnArea;
    private GameObject generateObject;
    private GameObject generateFolder;
    private int spawnCount;
    private Vector3 objectSize = new Vector3(0f, 0f, 0f);    // Увеличение / уменьшение коллизии обьектов генерации
    private Vector3 sizeArea = new Vector3(0f, 0f, 0f);      // Увеличение / уменьшение размеров зоны генерации
    public bool refreshGeneration = true;//
    private bool debugInfo = false;
    private double spawnСhance;
    public bool rotation = false;

    public GameObject SpawnArea { set { spawnArea = value; } get { return spawnArea; } }
    public GameObject GenerateObject { set { generateObject = value; } get { return generateObject; } }
    public GameObject GenerateFolder { set { generateFolder = value; } get { return generateFolder; } }
    public Vector3 ObjectSize { set { objectSize = value; } get { return objectSize; } }
    public Vector3 SizeArea { set { sizeArea = value; } get { return sizeArea; } }
    public bool DebugInfo { set { debugInfo = value; } get { return debugInfo; } }
    public int SpawnCount { set { spawnCount = value; } get { return spawnCount; } }
    public double SpawnСhance { set { spawnСhance = value; } get { return spawnСhance; } }

    List<Vector3> objectList;
    private bool spawn = true;
    private GameObject spawnedBuild;

    private Vector3 defaultObjectSize;
    private Vector3 defaultSizeArea;

    public void Generate()
    {
        Vector3 spawnPoint;
        //ObjectSize = defaultObjectSize;
        //SizeArea = defaultSizeArea;
        if(generateFolder.transform.childCount != 0)
            for (int i = 0; i < generateFolder.transform.childCount; i++)
            {
                Destroy(generateFolder.transform.GetChild(i).gameObject);
            }
        //Назначение верхней левой и нижней правой точки зоны генерации
        SizeArea += spawnArea.GetComponent<BoxCollider>().bounds.size;
        Vector3 areaTopLeftVertex = new Vector3(
            spawnArea.transform.position.x - sizeArea.x / 2,   //x верхней левой
            spawnArea.transform.position.y,                    //y
            spawnArea.transform.position.z + sizeArea.z / 2    //z
        );
        Vector3 areaBottomRightVertex = new Vector3(
            spawnArea.transform.position.x + sizeArea.x / 2,   //x нижней правой
            spawnArea.transform.position.y,                    //y
            spawnArea.transform.position.z - sizeArea.z / 2    //z
        );

        objectSize = new Vector3(
            objectSize.x + GenerateObject.GetComponent<BoxCollider>().bounds.size.x,
            objectSize.y + GenerateObject.GetComponent<BoxCollider>().bounds.size.y,
            objectSize.z + GenerateObject.GetComponent<BoxCollider>().bounds.size.z
        );


        //Создание списка разрешенных для генерации координат
        objectList = new List<Vector3> { getSpawnPoint(areaTopLeftVertex, areaBottomRightVertex) };
        if (debugInfo) Debug.Log("First object  " + objectList[0]);
        for (int i = 0; i < spawnCount; i++)
        {
            spawnPoint = getSpawnPoint(areaTopLeftVertex, areaBottomRightVertex);

            //Назначение верхней левой и нижней правой точки обьекта
            Vector3 firstObjTopLeftVertex = new Vector3(
                spawnPoint.x - objectSize.x / 2,                    //x верхней левой
                spawnArea.transform.position.y + objectSize.y / 2,  //y
                spawnPoint.z + objectSize.z / 2                     //z
            );
            Vector3 firstObjBottomRightVertex = new Vector3(
                spawnPoint.x + objectSize.x / 2,                    //x нижней правой
                spawnArea.transform.position.y + objectSize.y / 2,  //y
                spawnPoint.z - objectSize.z / 2                     //z
            );

            for (int j = 0; j < objectList.Count; j++)
            {
                //Назначение верхней левой и нижней правой точки обьекта
                Vector3 secondObjTopLeftVertex = new Vector3(
                    objectList[j].x - objectSize.x / 2,                     //x верхней левой
                    spawnArea.transform.position.y + objectSize.y / 2,      //y
                    objectList[j].z + objectSize.z / 2                      //z
                );
                Vector3 secondObjBottomRightVertex = new Vector3(
                    objectList[j].x + objectSize.x / 2,                     //x нижней правой
                    spawnArea.transform.position.y + objectSize.y / 2,      //y
                    objectList[j].z - objectSize.z / 2                      //z

                );

                if (debugInfo) Debug.Log(spawnPoint + " ?/ " + objectList[j] + "  =  " + intersects(firstObjTopLeftVertex, firstObjBottomRightVertex, secondObjTopLeftVertex, secondObjBottomRightVertex));

                //Проверка на пересечение обьектов генерации
                if (!intersects(
                    firstObjTopLeftVertex,
                    firstObjBottomRightVertex,
                    secondObjTopLeftVertex,
                    secondObjBottomRightVertex)
                    ) spawn = false;
            }
            //Добавление разрешенной координаты для спавна в список
            if (spawn) objectList.Add(spawnPoint);
            spawn = true;
        }
        
        //Генерация обьектов по списку разрешенных координат
        for (int i = 0; i < objectList.Count; i++)
        {
            if(getRandomChance() < spawnСhance)
            {
                if (!rotation) spawnedBuild = Instantiate(generateObject, objectList[i], Quaternion.identity);
                if (rotation) spawnedBuild = Instantiate(generateObject, objectList[i], getRandomRotation());
                spawnedBuild.transform.SetParent(generateFolder.transform);
            }
            
        }

        if (debugInfo) Debug.DrawLine(areaTopLeftVertex, areaBottomRightVertex);
    }

    //Метод возвращает случайную точку внутри зоны генерации(GameObject)
    private Vector3 getSpawnPoint(Vector3 areaTopLeftVertex, Vector3 areaBottomRightVertex)
    {
        Random rnd = new Random();
        Vector3 spawnBounds = new Vector3(
           rnd.Next((int)areaTopLeftVertex.x, (int)areaBottomRightVertex.x),     //x
            spawnArea.transform.position.y,                                      //y
            rnd.Next((int)areaBottomRightVertex.z, (int)areaTopLeftVertex.z)     //z
        );
        return spawnBounds;
    }
    private double getRandomChance()
    {
        Random rnd = new Random();
        return (double)rnd.Next(0,100)/100;
    }
    private Quaternion getRandomRotation()
    {
        Random rnd = new Random();
        return Quaternion.EulerRotation(0f,rnd.Next(0,180),0f);
    }
    //Метод определяющий пересечение двух BoxCollider
    private static bool intersects(Vector3 a,Vector3 a1, Vector3 b, Vector3 b1) {
        return (a.z<b1.z || a1.z> b.z || a1.x<b.x || a.x> b1.x );
    }
}
