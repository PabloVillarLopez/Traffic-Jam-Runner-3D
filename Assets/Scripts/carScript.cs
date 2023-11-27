using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : MonoBehaviour
{
    //Script para mover los barriles

    //Creamos una variable de tipo PlayerController para referenciar al Script PlayerController
    public PlayerController playerController;

    //Variable para mover el barril con rigidbody para tener precisión de físicas
    private Rigidbody myrigi;

    //Variable para darle una velocidad
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        //Hacemos que busque el script PlayerController
        playerController = FindObjectOfType<PlayerController>();

        //Declaramos el rigidbody accediendo a su componente para el movimiento por Rigidboy
        myrigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Finalmente, si el jugador tiene más de 0 vidas, es decir, no ha muerto, movemos el barril con Rigidbody con una velocidad con signo negativo para ir
         en la dirección del jugador y quedándose quieto cuando el jugador muere.*/
        if (PlayerController.health > 0 || Player2Controller.health > 0)
        {
            myrigi.velocity = new Vector3(myrigi.velocity.x, myrigi.velocity.y, -Speed);
        }
        else
        {
            myrigi.velocity = Vector3.zero;
        }


    }

    //Cuando cada barril haga Trigger con el muro, detrás del jugador, se destruirá
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
