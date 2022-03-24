using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    //boundary of x and y for camera to track player 
    public float boundX = 0.15f;
    public float boundY  = 0.05f;

    private void LateUpdate()
    {
       
        Vector3 delta = Vector3.zero;
        //check if within x boundary
        float deltaX = lookAt.position.x - transform.position.x;
        if(deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x  = deltaX - boundX;
                
            }
            else
            {
                delta.x  = deltaX + boundX;
            }
        }

        //check if within y boundary
        float deltaY = lookAt.position.y - transform.position.y;
        if(deltaY > boundY || deltaY < -boundY)
        {
            if(transform.position.y < lookAt.position.y)
            {
                delta.y  = deltaY - boundY;
                
            }
            else
            {
                delta.y  = deltaY + boundY;
            }
        }
        transform.position += new Vector3(delta.x, delta.y, 0);
        
    }
}
