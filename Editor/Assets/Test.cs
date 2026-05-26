using UnityEngine;
using Unity.Mathematics;

public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = transform.rotation;
        float3 f = q.eulerAngles;
        Debug.Log("X: " + f.x + ", Y: " + f.y + ", Z: " + f.z);
    }
}
