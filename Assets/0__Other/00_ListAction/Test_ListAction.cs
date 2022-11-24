using System;
using System.Collections.Generic;
using UnityEngine;

public class Test_ListAction : MonoBehaviour
{
    public static Test_ListAction Ins;
    // Start is called before the first frame update
    void Start()
    {
        Ins = this;
        List<Sub_ListAction> list = new List<Sub_ListAction>();
        for (int i = 0; i < 10; i++) {
            var aaa = new  Sub_ListAction();
            aaa.Init(i);
            list.Add(aaa);
        }
        Ins = this;
        for (int i = 9; i >=0; i-=2)
        {
            var aaa = list[i];
            aaa.Dispose();
            list.Remove(aaa);
        }
        foreach (var a in dic[1]) {
            a.Invoke();
        }
        Debug.Log($"dic count: {dic.Count}");
    }

    // Update is called once per frame
    void Update()
    {
          
    }
    Dictionary<int, List<Action>> dic = new Dictionary<int, List<Action>>();
    public void addListioner(int id,Action callback) {
        if (!dic.ContainsKey(id)) dic.Add(id, new List<Action>());
        dic[id].Add(callback);
    }
    public void RemoveListioner(int id,Action action) {
        dic[id].Remove(action);
    }   
}


public class Sub_ListAction {
    private int _id;
    private void display() {
        Debug.Log($"this id: {_id}");
    }
    public void Init(int id) {
        _id = id;
        Test_ListAction.Ins.addListioner(1, display);
    }

    public void Dispose()
    {
        Test_ListAction.Ins.RemoveListioner(1,display);
    }
}