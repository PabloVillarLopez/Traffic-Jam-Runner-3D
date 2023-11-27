using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float speed;
    private Rigidbody myRigi;

    public static float health;
    public static float points;

    // Start is called before the first frame update
    void Start()
    {
        myRigi = GetComponent<Rigidbody>();

        health = 8;
    }

    private void Update()
    {
        //Debug.Log("Vida jugador2: " + health);
        points += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //Prueba1

        //velocity = Input.GetAxis("Vertical");
        //myRigi.AddForce(new Vector3(0, 0, velocity) * acceleration * Time.deltaTime);
        //myRigi.velocity = Vector3.ClampMagnitude(myRigi.velocity, maxVelocity);

        //rotate = Input.GetAxis("Horizontal");
        //transform.Translate(new Vector2(rotate, 0) * rotationVelocity * Time.deltaTime);
        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -0.5f, 1.5f), transform.position.y);

        //Prueba que funciona controlado por el usuario

        //Movimiento hacia la izquierda
        if (Input.GetAxisRaw("LeftRight") < 0)
        {
            myRigi.velocity = new Vector3(-speed, myRigi.velocity.y, myRigi.velocity.z);
        }

        //Movimiento hacia la derecha
        if (Input.GetAxisRaw("LeftRight") > 0)
        {
            myRigi.velocity = new Vector3(speed, myRigi.velocity.y, myRigi.velocity.z);
        }

        //Movimiento est�tico para perder la inercia
        if (Input.GetAxisRaw("LeftRight") == 0)
        {
            myRigi.velocity = new Vector3(0, 0, 0);
        }



        //Movimiento hacia delante
        if (Input.GetAxisRaw("UpDown") > 0 && transform.position.z < 1.26f)
        {
            myRigi.velocity = new Vector3(myRigi.velocity.x, myRigi.velocity.y, speed);
        }

        //Movimiento hacia delante
        if (Input.GetAxisRaw("UpDown") < 0 && transform.position.z > 0.31f)
        {
            myRigi.velocity = new Vector3(myRigi.velocity.x, myRigi.velocity.y, -speed);
        }

        //Prueba que funciona aut�matico
        //myRigi.velocity = new Vector3(myRigi.velocity.x, myRigi.velocity.y, speed);
    }

    private void OnCollisionEnter(Collision collision) //Colisiones
    {
        if (collision.gameObject.tag == "obstacle1")    //Colisi�n con un misil quita una vida
        {
            health--;
        }

        if (collision.gameObject.tag == "obstacle2")    //Colisi�n con los obst�culos hecha el jugador hacia atr�s
        {
            myRigi.AddForce(new Vector3(0, 0, -1500));
        }


        /*if (collision.gameObject.tag == "powerUp") //PowerUp que sube una vida
        {
            health++;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        health = 0;
    }
}
