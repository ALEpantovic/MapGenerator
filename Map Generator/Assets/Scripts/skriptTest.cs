using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skriptTest : MonoBehaviour
{
    int x, y, z,zr,zrd = 0;
    [Range(0,10)]
    public float speed = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKey(KeyCode.W))
         {
             transform.Translate(new Vector3(zr, zrd, z - 1));
         }
         if (Input.GetKey(KeyCode.A))
         {
             transform.Rotate(new Vector3(0, 0, zr-1));
            zr = -1;
         }
         if (Input.GetKey(KeyCode.D))
         {
             transform.Rotate(new Vector3(0, 0, zrd+1));
            zrd = 1;
         }
        
    }
}
