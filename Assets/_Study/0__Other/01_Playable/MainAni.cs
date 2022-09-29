using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAni : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject person;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,33),"copy"))
        {
            StartCoroutine(Create());
        }
    }
    
    IEnumerator Create()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject.Instantiate(person, person.transform.parent);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
