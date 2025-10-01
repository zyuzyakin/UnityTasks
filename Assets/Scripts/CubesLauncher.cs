using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class CubesLauncher : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private int cubesCount;
    [SerializeField] private int space;
    [SerializeField] private int minForce;
    [SerializeField] private int maxForce;

    [SerializeField] private ScoreSummator summator;

    
    private bool isCharging = false;
    private float power;
    
    private GameObject[] cubes;
    private Rigidbody[] cubesRigids;

    private bool isMoving;
    private bool isCreated;

    public float Power
    {
        get => power;
        set
        {   
            if (value <= 1)
                power = value;
        }
    }

    public bool IsCharging
    {
        get => isCharging;
        set => isCharging = value;
    }
    public int CubesCount 
    { 
        get => cubesCount;
        set 
        { 
            cubesCount = value;
            summator.Range = $"Диапазон: {cubesCount}-{6*cubesCount}";
        } 
    }

    public void LaunchCubes()
    {
        summator.ResetResultValue();
        if (isCreated)
        {
            foreach (var cube in cubes)
                if (cube)
                    Destroy(cube);
            Debug.Log("Old Cubes destroyed");
            isCreated = false;
        }
        
        cubes = new GameObject[CubesCount];
        cubesRigids = new Rigidbody[CubesCount];

        var v = new Vector3();

        for (var i = 0; i < CubesCount; i++)
        {
            v = new Vector3(Random.value * space,
                Random.value * space, Random.value * space);

            cubes[i] = Instantiate(prefab, transform.position + v, Random.rotation);

            cubesRigids[i] = cubes[i].GetComponent<Rigidbody>();

            cubesRigids[i].AddForce(new Vector3(Random.value, Random.value, Random.value)
                * (minForce + (maxForce - minForce) * power), ForceMode.VelocityChange);

        }
        StartCoroutine(WaitForNextFrame());
        Debug.Log("Cubes launched");
    }
    
    public void OnCubeLaunch(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        LaunchCubes();
    }
    
    IEnumerator WaitForNextFrame()
    {
        yield return null;
        isCreated = true;
        isMoving = true;
    }

    

    private void FixedUpdate()
    {
        if (isCreated && isMoving)
        {   
            isMoving = false;
            foreach (var elem in cubesRigids)
            {
                if (elem.linearVelocity != Vector3.zero)
                {
                    isMoving = true;
                }
            }
            if (!isMoving)
            {
                Debug.Log("stop");
                summator.FinalizeScore();
            }
        }
    }
}
