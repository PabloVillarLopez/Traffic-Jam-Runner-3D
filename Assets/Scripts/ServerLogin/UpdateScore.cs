using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UpdateScore : MonoBehaviour
{
    public Server server;
    public string[] data = new string[3];
    //public GameObject loading;

    //Singleton
    public static UpdateScore instance;

    public void Awake() //Para patrón de diseño Singleton, cuando se carga una nueva escena,    
    {


        if (instance == null) //se verifica si ya existe una instancia de Update Score.
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Si no existe, se crea una y se marca como "DontDestroyOnLoad", lo que significa que no se destruirá cuando se cargue una nueva escena.
        }
        else
        {
            Destroy(gameObject); //Si ya existe una instancia, se destruye la nueva instancia para evitar duplicados.
        }
    }

    public void UpdateScorePlayer()
    {
        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        //loading.SetActive(true);
        
        //data[0] = Login.user;
        data[2] = PlayerController.finalPoints.ToString();
        StartCoroutine(server.UseService("update user", data, PostLoad));
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => !server.busy);
        //loading.SetActive(false);
    }

    void PostLoad()
    {
        switch (server.respuesta.codigo)
        {
            case 207: //Puntuacion actualizada con exito
                Login.instance.usuario = JsonUtility.FromJson<DBUser>(server.respuesta.respuesta);
                //Login.instance.usuario.puntuacion = data[1];
                Debug.Log(server.respuesta.mensaje);
                //SceneManager.LoadScene("Prueba");
                break;
            case 208: //Puntuacion no actualizada
                Debug.Log(server.respuesta.mensaje);
                break;
            case 402: //Faltan datos para ejecutar la acción solicitada
                Debug.Log(server.respuesta.mensaje);
                break;
            case 400: //Error intentando conectar
                Debug.Log("Error, connection with the server lost");
                break;
            default:
                break;
        }
    }
}
