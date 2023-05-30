using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class UI_Game : MonoBehaviour
{
    DataTable table;
    public GameObject Player_Unit;

    public GameObject Score;
    public GameObject High_Score;
    public GameObject Ammo_Count;
    public GameObject HP_Count;

    private TextMeshProUGUI textScore;
    private TextMeshProUGUI textHighScore;
    private TextMeshProUGUI textAmmoCount;
    private TextMeshProUGUI textHpCount;

    private Player playerUnit;
    private Gun_shoot playerShoot;
    // Start is called before the first frame update
    void Start()
    {
        playerUnit = Player_Unit.transform.GetComponent<Player>();
        playerShoot = Player_Unit.GetComponentInChildren<Gun_shoot>();
        table = Database.GetTable("SELECT [High_score] FROM User;");

        textScore = Score.GetComponent<TextMeshProUGUI>();
        textHighScore = High_Score.GetComponent<TextMeshProUGUI>();
        textAmmoCount = Ammo_Count.GetComponent<TextMeshProUGUI>();
        textHpCount = HP_Count.GetComponent<TextMeshProUGUI>();

        textHighScore.text = "High: "+ table.Rows[0][0].ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score: " + playerUnit.score.ToString();
        textHighScore.text = "High: " + playerUnit.HighScore.ToString();
        if (playerShoot.isActiveReload)
             textAmmoCount.text = "Ammo: Reloading...";
        else textAmmoCount.text = "Ammo: " + playerShoot.AmmoLeft.ToString();
        textHpCount.text = "HP: "+ playerUnit.unit.HealthPoint.ToString();
        
    }
}
