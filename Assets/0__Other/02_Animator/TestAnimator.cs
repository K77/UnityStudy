using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject avatar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,100),""))
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject.Instantiate(avatar, avatar.transform.parent);
            }
        }
    }
}
