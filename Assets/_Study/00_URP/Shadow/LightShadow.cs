using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShadow : MonoBehaviour
{
    public Camera main;
    public GameObject postObj;
    private int count = 10;
    public GameObject obj;

    private List<Transform> _list = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = -count; i < count; i++)
        {
            for (int j = -count; j < count; j++)
            {
                GameObject tmp = GameObject.Instantiate(obj, obj.transform.position + (new Vector3(i, 0, j))/0.5f,
                    Quaternion.identity, obj.transform.parent);
                _list.Add(tmp.transform);
            }
        }
    }

    private int countTmp = 0;
    // Update is called once per frame
    void Update()
    {
        countTmp++;
        _list[countTmp % _list.Count].position += new Vector3(0, 0.1f, 0);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(1,1,100,100),""))
        {
            postObj.SetActive(!postObj.activeSelf);
            // main.renderer
        }
    }
}
