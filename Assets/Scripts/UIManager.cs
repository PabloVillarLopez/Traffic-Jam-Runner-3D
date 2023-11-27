using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI redTeamPointsText;
    public TextMeshProUGUI blueTeamPointsText;
    public TextMeshProUGUI blueTeamHealthText;
    public TextMeshProUGUI redTeamHealthText;
    public GameObject panelDeathLeft;
    public GameObject panelDeathRight;
    public Button restartButton;
    public Button mainMenuButton;

    public TextMeshProUGUI blueTeamDeathPoints;
    public TextMeshProUGUI redTeamDeathPoints;

    // Start is called before the first frame update
    void Start()
    {
        panelDeathLeft.SetActive(false);
        panelDeathRight.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        redTeamPointsText.text = "Red points: " + Player2Controller.points.ToString("f0");
        blueTeamPointsText.text = "Blue points: " + PlayerController.finalPoints.ToString();
        blueTeamHealthText.text = "Blue Health: " + PlayerController.health.ToString();
        redTeamHealthText.text = "Red Health:" + Player2Controller.health.ToString();

        if (PlayerController.health < 1)
        {
            blueTeamPointsText.gameObject.SetActive(false);
            blueTeamHealthText.gameObject.SetActive(false);
            panelDeathLeft.SetActive(true);
            blueTeamDeathPoints.text = "Final blue points: " + Login.instance.usuario.puntuacion;
        }

        if (Player2Controller.health < 1)
        {
            redTeamPointsText.gameObject.SetActive(false);
            redTeamHealthText.gameObject.SetActive(false);
            panelDeathRight.SetActive(true);
            redTeamDeathPoints.text = "Final red points: " + StatsManager.redPlayerPoints.ToString("f0");
        }

        if (PlayerController.health < 1 && Player2Controller.health < 1)
        {
            restartButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        PlayerController.points = 0;
        Player2Controller.points = 0;
        panelDeathLeft.SetActive(false);
        panelDeathRight.SetActive(false);
        restartButton.gameObject.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        PlayerController.points = 0;
        Player2Controller.points = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
