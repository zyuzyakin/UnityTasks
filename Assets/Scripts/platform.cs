using System.Collections;
using UnityEngine;

public class platform : MonoBehaviour
{
    
    private int s;

    private void OnTriggerEnter(Collider other)
    {       
        s += 7 - other.gameObject.GetComponent<Face>().Value;

    }
    private void OnTriggerExit(Collider other)
    {

        s -= 7 - other.gameObject.GetComponent<Face>().Value;
           

    }
    private void FixedUpdate()
    {
        Debug.Log(s);
    }
}
