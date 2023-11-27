using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    //Script para instanciar múltiples barriles, PowerUps y torretas a través de Perlin Noise haciendo uso de la probabilidad

    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;


    public int width = 256;
    public int height = 256;

    public float scale = 20f;
    public float offsetX = 100f;
    public float offsetY = 100f;
    public float velocity = 5f;

    public Vector3 correction;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = Random.Range(0f, 99999f);
        offsetY = Random.Range(0f, 99999f);
        if (PlayerController.health > 0 || Player2Controller.health > 0)
        {
            InvokeRepeating("GenerateWalls", 1f, 1f);
        }
        /*else if (PlayerController.health <= 0 && Player2Controller.health <= 0)
        {
            Destroy(gameObject);
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        offsetY += velocity * Time.deltaTime;

        if (StatsManager.bluePlayerHealth <= 0 && StatsManager.redPlayerHealth <= 0)
        {
            CancelInvoke();
        }
    }

    void GenerateWalls()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float valor = CalculateWall(x, y);
                /*if (DifficultyGameManagementMenu.easyMode)
                {
                    if (valor <= 0.1f)
                    {
                        GameObject clon = Instantiate(cubo, new Vector3(x, y), Quaternion.identity) as GameObject;
                    }
                    else
                    {

                        float aleatorio = Random.Range(0f, 100f);
                        if (aleatorio < 1f)
                        {
                            GameObject clon2 = Instantiate(torreta, new Vector3(x, y), Quaternion.identity) as GameObject;
                        }
                        else if (aleatorio > 97.5f)
                        {
                            GameObject clon = Instantiate(powerUpobject, new Vector3(x, y), Quaternion.identity) as GameObject;
                        }


                    }
                }*/

                //if (DifficultyGameManagementMenu.intermediateMode)
                //{
                    if (valor <= 0.23f)
                    {
                        GameObject clon = Instantiate(obstacle1, new Vector3(x, y) + correction, Quaternion.identity) as GameObject;
                    }
                    else
                    {

                        float aleatorio = Random.Range(0f, 100f);
                        if (aleatorio < 3f)
                        {
                            GameObject clon2 = Instantiate(obstacle2, new Vector3(x, y) + correction, Quaternion.identity) as GameObject;
                        }
                        else if (aleatorio > 98.5f)
                        {
                            GameObject clon = Instantiate(obstacle3, new Vector3(x, y) + correction, Quaternion.identity) as GameObject;
                        }


                    }
                //}


                /*if (DifficultyGameManagementMenu.hardMode)
                {
                    if (valor <= 0.3f)
                    {
                        GameObject clon = Instantiate(cubo, new Vector3(x, y), Quaternion.identity) as GameObject;
                    }
                    else
                    {

                        float aleatorio = Random.Range(0f, 100f);
                        if (aleatorio < 5f)
                        {
                            GameObject clon2 = Instantiate(torreta, new Vector3(x, y), Quaternion.identity) as GameObject;
                        }
                        else if (aleatorio > 98f)
                        {
                            GameObject clon = Instantiate(powerUpobject, new Vector3(x, y), Quaternion.identity) as GameObject;
                        }


                    }
                }*/

            }
        }
    }

    float CalculateWall(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return sample;
    }
}
