using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Server", menuName = "Server", order = 1)]
public class Server : ScriptableObject
{
    public string server;
    public Service[] services;

    public bool busy = false;
    public Respuesta respuesta;

    public IEnumerator UseService(string name, string[] data, UnityAction e)
    {
        busy = true;
        WWWForm form = new WWWForm();
        Service service = new Service();
        for (int i = 0; i < services.Length; i++)
        {
            if (services[i].name.Equals(name))
            {
                service = services[i];
            }
        }

        for (int i = 0; i < service.parameters.Length; i++)
        {
            form.AddField(service.parameters[i], data[i]);
        }

        UnityWebRequest www = UnityWebRequest.Post(server + "/" + service.URL, form);
        Debug.Log(server + "/" + service.URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            respuesta = new Respuesta();
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            respuesta = JsonUtility.FromJson<Respuesta>(www.downloadHandler.text);
            respuesta.ClearAnswer();

            //Debug.Log(answer);
        }

        busy = false;
        e.Invoke();
    }
}

[System.Serializable]
public class Service
{
    public string name;
    public string URL;
    public string[] parameters;
}

[System.Serializable]
public class Respuesta
{
    public int codigo;
    public string mensaje;
    public string respuesta;

    public void ClearAnswer()
    {
        respuesta = respuesta.Replace('#', '"');
    }

    public Respuesta()
    {
        codigo = 404;
        mensaje = "Error";
    }

}

[System.Serializable]
public class DBUser
{
    public int id;
    public string usuario;
    public string contrasena;
    public int puntuacion;
}