using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class cubesLaunch : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private int minForce;
    [SerializeField] private int maxForce;


    private GameObject[] cubes = new GameObject[3];

    private Rigidbody[] rigids = new Rigidbody[3];


    public void OnCubeLaunch(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Debug.Log("launch!!!");

        Vector3 v;

        for (var i = 0; i < 3; i++)
        {
            v = new Vector3(Random.value * 10, Random.value * 10, Random.value * 10);

            cubes[i] = Instantiate(prefab, transform.position + v, Random.rotation);

            rigids[i] = cubes[i].GetComponent<Rigidbody>();
            rigids[i].AddForce(new Vector3(Random.value, Random.value, Random.value) 
                * (minForce + (maxForce-minForce) * Random.value), ForceMode.Impulse);
            
        }
    }
}
