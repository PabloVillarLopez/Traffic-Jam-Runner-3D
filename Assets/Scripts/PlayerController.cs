using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody myRigi;

    public static float health;
    public static float points;
    public static int finalPoints;

    // Start is called before the first frame update
    void Start()
    {
        myRigi = GetComponent<Rigidbody>();

        health = 8;
    }

    private void Update()
    {
        points += 1 * Time.deltaTime;
        finalPoints = Login.instance.usuario.puntuacion + Mathf.RoundToInt(points);
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
        if (Input.GetAxisRaw("AD") < 0)
        {
            myRigi.velocity = new Vector3(-speed, myRigi.velocity.y, myRigi.velocity.z);
        }

        //Movimiento hacia la derecha
        if (Input.GetAxisRaw("AD") > 0)
        {
            myRigi.velocity = new Vector3(speed, myRigi.velocity.y, myRigi.velocity.z);
        }

        //Movimiento estático para perder la inercia
        if (Input.GetAxisRaw("AD") == 0)
        {
            myRigi.velocity = new Vector3(0, 0, 0);
        }



        //Movimiento hacia delante
        if (Input.GetAxisRaw("WS") > 0 && transform.position.z < 1.26f)
        {
            myRigi.velocity = new Vector3(myRigi.velocity.x, myRigi.velocity.y, speed);
        }

        //Movimiento hacia delante
        if (Input.GetAxisRaw("WS") < 0 && transform.position.z > 0.31f)
        {
            myRigi.velocity = new Vector3(myRigi.velocity.x, myRigi.velocity.y, -speed);
        }

        //Prueba que funciona autómatico
        //myRigi.velocity = new Vector3(myRigi.velocity.x, myRigi.velocity.y, speed);
    }

    private void OnCollisionEnter(Collision collision) //Colisiones
    {
        if (collision.gameObject.tag == "obstacle1")    //Colisión con un misil quita una vida
        {
            health--;
        }

        if (collision.gameObject.tag == "obstacle2")    //Colisión con los obstáculos hecha el jugador hacia atrás
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
        StatsManager.bluePlayerPoints = points;
        UpdateScore.instance.UpdateScorePlayer();
    }
}
