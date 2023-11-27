using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadControl : MonoBehaviour
{
    public GameObject[] roadsArray;
    public Vector3 correction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstanciarCarreteras(Vector3 previousPosition)
    {
        int randomNumber = Random.Range(0, roadsArray.Length);
        GameObject newRoad = Instantiate(roadsArray[randomNumber], previousPosition + correction, Quaternion.identity);
    }
}
