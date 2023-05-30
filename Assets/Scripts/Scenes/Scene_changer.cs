using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_changer : MonoBehaviour
{
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            isActive = true;
        }
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("HubScene");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive) return;
        else
        {
            Debug.Log("Loading Scene!");
            SceneManager.LoadScene("Game_Scene");
            isActive = false;
        }
    }
}
