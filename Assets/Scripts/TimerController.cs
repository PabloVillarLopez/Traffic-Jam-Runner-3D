using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public int min, seg;
    public TMP_Text tiempo;

    private float restante;
    private float playerTime;
    private bool enMarcha = true;

    private void Awake()
    {
        restante = (min * 60) + seg;
    }

    // Update is called once per frame
    void Update()
    {
        if (enMarcha == true)
        {
            restante -= Time.deltaTime;
            if (restante <= 0)
            {
                enMarcha = false;

                SceneManager.LoadScene("MainMenu");
            }
            int tiempoMin = Mathf.FloorToInt(restante / 60);
            int tiempoSeg = Mathf.FloorToInt(restante % 60);

            tiempo.text = string.Format("{00:00}:{01:00}", tiempoMin, tiempoSeg);
        }

        if (PlayerController.health < 1 && PlayerController.health < 1)
        {
            enMarcha = false;
        }

    }
}
