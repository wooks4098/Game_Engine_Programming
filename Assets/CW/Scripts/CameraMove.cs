using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    void Update()
    {
        moveObjectFunc();
    }

    Vector3 forward = new Vector3(0,0,1);
    Vector3 right = new Vector3(1,0,0);
    private float speed_move = 3.0f;
    private float speed_rota = 2.0f;

    void moveObjectFunc()
    {
        float keyH = Input.GetAxis("Horizontal");
        float keyV = Input.GetAxis("Vertical");
        keyH = keyH * speed_move * Time.deltaTime;
        keyV = keyV * speed_move * Time.deltaTime;
        //transform.Translate(Vector3.right * keyH);
       // transform.Translate(Vector3.forward * keyV);
        transform.position += right * keyH;
        transform.position += forward * keyV;

       //float mouseX = Input.GetAxis("Mouse X");
       //float mouseY = Input.GetAxis("Mouse Y");
       //transform.Rotate(Vector3.up * speed_rota * mouseX);
       //transform.Rotate(Vector3.left * speed_rota * mouseY);
    }
}
