using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TestNet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var socket = new Socket(new SocketInformation()) {NoDelay = true};
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
