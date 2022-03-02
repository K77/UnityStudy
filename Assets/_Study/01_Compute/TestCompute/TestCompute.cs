using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCompute : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }
    public ComputeShader csBuffer;
    ComputeBuffer buffer;
    struct MyInt{
        public int val;
        public int index;
    };

    void Start()
    {
       
        CSFib();
        
    }

    public void CSFib(){
        MyInt[] total = new MyInt[32];
        buffer = new ComputeBuffer(32,8);
        int kernel = csBuffer.FindKernel("Fibonacci");
        csBuffer.SetBuffer(kernel,"buffer",buffer);
        csBuffer.Dispatch(kernel,1,1,1);
        buffer.GetData(total);
        for (int i = 0; i < total.Length; i++)
        {
            Debug.Log(total[i].val);
        }

    }

    private void OnDestroy() {
        buffer.Release();
    }
}
