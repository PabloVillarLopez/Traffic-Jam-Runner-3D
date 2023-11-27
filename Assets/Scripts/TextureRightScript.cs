using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRightScript : MonoBehaviour
{
    private Material myMaterial;
    public float textureMovementX;
    public float textureMovementY;

    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player2Controller.health > 0)
        {
            myMaterial.SetTextureOffset("_MainTex", new Vector2(textureMovementX, textureMovementY));

            textureMovementY += textureMovementY * Time.deltaTime * 0.1f;
        }
        


    }
}
