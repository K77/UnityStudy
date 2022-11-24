using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDelegateRemove : MonoBehaviour
{
    private delegate void DVoidVoid();
    private Action<string> bbb;
    private DVoidVoid aaa;
    
    // Start is called before the first frame update
    void Start()
    {
        // aaa += aaa1;
        // aaa += aaa2;
        // aaa += aaa3;
        // aaa += aaa4;
        // // aaa -= aaa1;
        //
        // aaa.Invoke();
        bbb += print1;
        bbb += print2;
        bbb += print3;
        bbb += print4;
        bbb("aaaaa");
    }

    // Update is called once per frame
    void Update()
    {
        bbb("bbbbb");
        // aaa.Invoke();
    }

    void aaa1()
    {
        print1("a1");
        aaa -= aaa1;
        aaa -= aaa2;
    }
    void aaa2()
    {
        print1("a2");
    }
    void aaa3()
    {
        print1("a3");
    }
    void aaa4()
    {
        print1("a4");
    }

    void print1(string a)
    {
        Debug.Log("1111"+a);
        bbb -= print1;
        bbb -= print2;
    }
    void print2(string a)
    {
        Debug.Log("222"+a);
    }
    void print3(string a)
    {
        Debug.Log("333"+a);
    }
    void print4(string a)
    {
        Debug.Log("444"+a);
    }
}