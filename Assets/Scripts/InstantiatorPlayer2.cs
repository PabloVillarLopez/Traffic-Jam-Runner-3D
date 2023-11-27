using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorPlayer2 : MonoBehaviour
{
    public Vector3 roadPosition;

    public GameObject[] roadsArray;
    public Vector3 correction;

    // Start is called before the first frame update
    void Start()
    {
        roadPosition = this.gameObject.transform.position;
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coche2"))
        {
            Debug.Log("Contacto");
            InstanciarCarreteras(roadPosition);
        }
        else if (other.gameObject.CompareTag("Destroyer"))
        {
            Destroy(this.gameObject);
        }
    }

    public void InstanciarCarreteras(Vector3 previousPosition)
    {
        int randomNumber = Random.Range(0, roadsArray.Length);
        GameObject newRoad = Instantiate(roadsArray[randomNumber], previousPosition + correction, Quaternion.identity);
    }
}
