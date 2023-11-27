using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static WebSaveLoadJson.estructuraDeDatos;

public class WebSaveLoadJson : MonoBehaviour
{
    [System.Serializable]
    public struct estructuraDeDatos //Como una clase per más simple
    {
        //Variables del juego para guardar
        [System.Serializable]
        public struct registroUsers 
        {
            public string nombre;
            public string contrasena;
            public int puntos;
        }
        public registroUsers[] registros;
        public string fecha;
    }

    public estructuraDeDatos datos;

    //
    //Leer directamente desde la web con json
    [ContextMenu("LeerJsonWeb")]

    void LeersonWeb()
    {
        StartCoroutine(CorrutinaJsonLeer());
    }

    IEnumerator CorrutinaJsonLeer()
    {
        UnityWebRequest web = UnityWebRequest.Get("http://localhost/loginunity/pruebajson.txt");
        yield return web.SendWebRequest();

        if ((web.result == UnityWebRequest.Result.ConnectionError) || (web.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError("No se han podido recuperar los datos");
        }
        else
        {
            Debug.Log(web.downloadHandler.text);
            datos = JsonUtility.FromJson<estructuraDeDatos>(web.downloadHandler.text);
        }
    }
    //

    //
    //Escribir varios datos con json
    [ContextMenu("EscribirJsonWeb")]

    void EscribirJsonWeb()
    {
        StartCoroutine(CorrutinaJsonEscribir());
    }

    IEnumerator CorrutinaJsonEscribir()
    {
        //Creamos un formulario nuevo y asignamos las variables que vamos a guardar en el PHP
        WWWForm form = new WWWForm();
        form.AddField("archivo", "pruebajson.txt");
        form.AddField("texto", JsonUtility.ToJson(datos));

        //Guardamos las variables en el PHP
        UnityWebRequest web = UnityWebRequest.Post("http://localhost/loginunity/server.php", form);

        yield return web.SendWebRequest();

        if (!web.isNetworkError && !web.isHttpError)
        {
            Debug.Log(web.downloadHandler.text);
        }
        else
        {
            Debug.LogError("No se han podido recuperar los datos");
        }
    }
    //
}
