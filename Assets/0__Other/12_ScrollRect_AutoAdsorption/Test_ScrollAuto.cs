using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_ScrollAuto : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private RectTransform txt;

    private List<string> _data = new List<string>();
    void Start()
    {
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,222*20);
        for (int i = 0; i < 20; i++)
        {
            var tmp = GameObject.Instantiate(txt, content);
            tmp.GetComponent<Text>().text = i.ToString();
            tmp.anchoredPosition = Vector2.down*i*222;
        }
        txt.gameObject.SetActive(false);
        var aa = gameObject.AddComponent<ScrollRect_AutoAdsorption>();
        aa.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
