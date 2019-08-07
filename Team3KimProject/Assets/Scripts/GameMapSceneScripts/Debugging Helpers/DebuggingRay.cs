using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggingRay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Click on right mouse button
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);
                //Debug.Log(hit.transform.tag);
                //Destroy(hit.transform.gameObject); //If you want to destroy. Turn it off midgame to check effects.
            }
        }
    }
}
