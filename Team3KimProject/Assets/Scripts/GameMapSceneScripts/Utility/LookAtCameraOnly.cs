using UnityEngine;

using System.Collections;

public class LookAtCameraOnly : MonoBehaviour

{

    private Camera cameraToLookAt;

    void Start()
    {
        //transform.Rotate( 180,0,0 );
        cameraToLookAt = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        //v.x = v.z = 0.0f; //For x,z  //v.y = 0.0f; //y doesn't work as planned.
        //transform.LookAt(cameraToLookAt.transform.position - v); //This is if you want the canvas to be rotating dinamically.

        transform.Rotate(0, 180, 180);
    }

}