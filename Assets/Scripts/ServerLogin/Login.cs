using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public Server server;
    private GameObject panelInputUser;
    private GameObject panelInputPassword;
    private TMP_InputField inputUser;
    private TMP_InputField inputPassword;
    private GameObject loading;
    public DBUser usuario;

    private GameObject loginButtonPanel;
    private Button loginButton;

    //public static string user;

    //Singleton
    public static Login instance;

    public void Awake() //Para patrón de diseño Singleton, cuando se carga una nueva escena,    
    {
        

        if (instance == null) //se verifica si ya existe una instancia de Login.
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Si no existe, se crea una y se marca como "DontDestroyOnLoad", lo que significa que no se destruirá cuando se cargue una nueva escena.
        }
        else
        {
            Destroy(gameObject); //Si ya existe una instancia, se destruye la nueva instancia para evitar duplicados.
        }

        //loading = GameObject.FindGameObjectWithTag("loading");
        //loading.SetActive(false);
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "GameScene")
        {
            StopAllCoroutines();
        }

        if (scene.name == "MainMenu")
        {
            //Para buscar automáticamente el botón de login y ponerle el evento en el onclick
            loginButtonPanel = GameObject.FindGameObjectWithTag("loginButton");
            loginButton = loginButtonPanel.GetComponentInChildren<Button>();
            loginButton.onClick.AddListener(LogInUser);

            loading = GameObject.FindGameObjectWithTag("loading");
            loading.SetActive(false);
            panelInputUser = GameObject.FindGameObjectWithTag("panelinputuser");
            panelInputPassword = GameObject.FindGameObjectWithTag("panelinputpassword");
            inputUser = panelInputUser.GetComponentInChildren<TMP_InputField>();
            inputPassword = panelInputPassword.GetComponentInChildren<TMP_InputField>();
        }

    }

    public void LogInUser()
    {
        StartCoroutine(Enter());
    }

    IEnumerator Enter()
    {
        loading.SetActive(true);
        string[] data = new string[2];
        data[0] = inputUser.text;
        data[1] = inputPassword.text;
        StartCoroutine(server.UseService("login", data, PostLoad));
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.busy);
        loading.SetActive(false);
    }

    void PostLoad()
    {
        switch (server.respuesta.codigo)
        {
            case 204: //Usuario o contraseña incorrectos
                Debug.Log(server.respuesta.mensaje);
                break;
            case 205: //Inicio de sesión correcto
                usuario = JsonUtility.FromJson<DBUser>(server.respuesta.respuesta);
                UpdateScore.instance.data[0] = usuario.usuario;
                UpdateScore.instance.data[1] = usuario.contrasena;
                SceneManager.LoadScene("GameScene");
                //Debug.Log(server.answer.mensaje);
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
