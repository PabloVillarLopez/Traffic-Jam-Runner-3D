using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EditPassword : MonoBehaviour
{
    public Server server;
    public TMP_InputField inputUser;
    public TMP_InputField inputPassword;
    public TMP_InputField inputNewPassword;
    public GameObject editUserSucessfully;
    //public DBUser usuario;

    public GameObject changePasswordPanel;
    //public Button editPasswordButton;

    private bool changePasswordPanelActive = false;

    private void Start()
    {
        editUserSucessfully.SetActive(false);
        changePasswordPanel.SetActive(false);
    }

    public void ShowChangePasswordPanel()
    {
        changePasswordPanelActive = !changePasswordPanelActive;
        changePasswordPanel.SetActive(changePasswordPanelActive);
    }

    public void EditPasswordUser()
    {
        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        string[] data = new string[3];
        data[0] = inputUser.text;
        data[1] = inputPassword.text;
        data[2] = inputNewPassword.text;
        StartCoroutine(server.UseService("edit user", data, PostLoad));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.busy);
    }

    void PostLoad()
    {
        switch (server.respuesta.codigo)
        {
            case 204: //Usuario o contraseña incorrectos
                Debug.Log(server.respuesta.mensaje);
                break;
            case 206: //Usuario editado con éxito
                Login.instance.usuario = JsonUtility.FromJson<DBUser>(server.respuesta.respuesta);
                StartCoroutine(ShowPanelWaitHide());
                changePasswordPanel.SetActive(false);
                //Debug.Log(server.answer.mensaje);
                break;
            case 402: //Faltan datos para ejecutar la acción solicitada
                Debug.Log(server.respuesta.mensaje);
                break;
            case 400: //Error
                Debug.Log("Error, connection with the server lost");
                break;
            default:
                break;
        }
    }

    IEnumerator ShowPanelWaitHide()
    {
        editUserSucessfully.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        editUserSucessfully.SetActive(false);
    }
}
