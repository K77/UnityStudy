using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrAccelerometer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.acceleration.x;
        float y = Input.acceleration.y;
        float z = Input.acceleration.z;
        Debug.Log("(" + x + " , " + y + " , " + z + ")");
    }
}
