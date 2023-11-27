using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebSaveLoad : MonoBehaviour
{
    [System.Serializable]
    public struct estructuraDeDatos //Como una clase per más simple
    {
        //Variables del juego para guardar
        public string nombre;
        public int puntos;
    }

    public estructuraDeDatos datos;


    //
    //Leer directamente un .txt desde la web
    [ContextMenu("LeerSimpleWeb")]

    void LeerSimpleWeb()
    {
        StartCoroutine(CorrutinaSimpleLeer());
    }

    IEnumerator CorrutinaSimpleLeer()
    {
        UnityWebRequest web = UnityWebRequest.Get("http://localhost/game/pruebaescribirsimple.txt");
        yield return web.SendWebRequest();

        if ((web.result == UnityWebRequest.Result.ConnectionError) || (web.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError("No se han podido recuperar los datos");
        }
        else
        {
            Debug.Log(web.downloadHandler.text);
        }
    }
    //


    //
    //Escribir a un .txt desde la web
    [ContextMenu("EscribirSimpleWeb")]

    void EscribirSimpleWeb()
    {
        StartCoroutine(CorrutinaSimpleEscribir());
    }

    IEnumerator CorrutinaSimpleEscribir()
    {
        //Creamos un formulario nuevo y asignamos las variables que vamos a guardar en el PHP
        WWWForm form = new WWWForm();
        form.AddField("archivo","pruebaescribirsimple.txt");
        form.AddField("texto","Esto es una prueba de escritura desde Unity.");

        //Guardamos las variables en el PHP
        UnityWebRequest web = UnityWebRequest.Post("http://localhost/game/server.php", form);

        yield return web.SendWebRequest();

        if ((web.result == UnityWebRequest.Result.ConnectionError) || (web.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError("No se han podido recuperar los datos");
        }
        else
        {
            Debug.Log(web.downloadHandler.text);
        }
    }
    //


    //
    //Leer directamente un .txt desde la web
    [ContextMenu("LeerSinJsonWeb")]

    void LeerSinJsonWeb()
    {
        StartCoroutine(CorrutinaSinJsonLeer());
    }

    IEnumerator CorrutinaSinJsonLeer()
    {
        UnityWebRequest web = UnityWebRequest.Get("http://localhost/game/pruebavariossinjson.txt");
        yield return web.SendWebRequest();

        if ((web.result == UnityWebRequest.Result.ConnectionError) || (web.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError("No se han podido recuperar los datos");
        }
        else
        {
            Debug.Log(web.downloadHandler.text);

            string textoOrigen = web.downloadHandler.text;
            string[] partes = textoOrigen.Split('ç');
            datos.nombre = partes[0];
            datos.puntos = int.Parse(partes[1]);
        }
    }
    //

    //
    //Escribir varios datos sin json
    [ContextMenu("EscribirSinJsonWeb")]

    void EscribirSinJsonWeb()
    {
        StartCoroutine(CorrutinaSinJsonEscribir());
    }

    IEnumerator CorrutinaSinJsonEscribir()
    {
        //Creamos un formulario nuevo y asignamos las variables que vamos a guardar en el PHP
        WWWForm form = new WWWForm();
        form.AddField("archivo", "pruebavariossinjson.txt");
        form.AddField("texto", datos.nombre + "ç" + datos.puntos.ToString());

        //Guardamos las variables en el PHP
        UnityWebRequest web = UnityWebRequest.Post("http://localhost/game/server.php", form);

        yield return web.SendWebRequest();

        if ((web.result == UnityWebRequest.Result.ConnectionError) || (web.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.LogError("No se han podido recuperar los datos");
        }
        else
        {
            Debug.Log(web.downloadHandler.text);
      }
    }
    //
}
