using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    DataTable table;
    public GameObject Player_Unit;

    public GameObject Score;
    public GameObject High_Score;

    private TextMeshProUGUI textScore;
    private TextMeshProUGUI textHighScore;

    private Player playerUnit;
    private GameObject panel;
    private GameObject robber_animation;
    private GameObject police_animation;

    private int i;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        panel = gameObject.transform.GetChild(0).gameObject;
        robber_animation = gameObject.transform.GetChild(1).gameObject;
        police_animation = gameObject.transform.GetChild(2).gameObject;
        panel.SetActive(false);
        robber_animation.SetActive(false);
        police_animation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerUnit = Player_Unit.transform.GetComponent<Player>();
        if (playerUnit.unit.lifeStatus == false && i == 0)
        {i++;
            
            panel.SetActive(true);
            table = Database.GetTable("SELECT [High_score] FROM User;");

            textScore = Score.GetComponent<TextMeshProUGUI>();
            textHighScore = High_Score.GetComponent<TextMeshProUGUI>();

            textScore.text = "Score: " + playerUnit.score.ToString();
            textHighScore.text = "High Score: " + playerUnit.HighScore.ToString();
            if (playerUnit.score == playerUnit.HighScore)
                robber_animation.SetActive(true);
            else
                police_animation.SetActive(true);
        }
    }
}
