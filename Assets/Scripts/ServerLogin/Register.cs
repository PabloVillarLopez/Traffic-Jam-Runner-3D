using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public Server server;
    public TMP_InputField inputUser;
    public TMP_InputField inputPassword;
    public GameObject loading;

    public void RegisterUser()
    {
        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        loading.SetActive(true);
        string[] data = new string[2];
        data[0] = inputUser.text;
        data[1] = inputPassword.text;
        StartCoroutine(server.UseService("register user", data, PostLoad));
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => !server.busy);
        loading.SetActive(false);
    }

    void PostLoad()
    {
        switch (server.respuesta.codigo)
        {
            case 201: //Usuario creado correctamente
                Debug.Log(server.respuesta.mensaje);
                //SceneManager.LoadScene("Prueba");
                break;
            case 401: //Error intentando crear el usuario
                Debug.Log(server.respuesta.mensaje);
                break;
            case 402: //Faltan datos para ejecutar la acción solicitada
                Debug.Log(server.respuesta.mensaje);
                break;
            case 404: //Error
                Debug.Log("Error, connection with the server lost");
                break;
            default:
                break;
        }
    }
}
