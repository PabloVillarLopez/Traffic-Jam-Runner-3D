using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static float redPlayerHealth;
    public static float redPlayerPoints;
    public static float bluePlayerHealth;
    public static float bluePlayerPoints;

    // Update is called once per frame
    void Update()
    {
        redPlayerHealth = Player2Controller.health;
        redPlayerPoints = Player2Controller.points;
        bluePlayerHealth = PlayerController.health;
        bluePlayerPoints = PlayerController.points;
    }
}
