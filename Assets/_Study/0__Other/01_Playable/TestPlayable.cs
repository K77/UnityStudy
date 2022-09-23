//foot ik 可以防止脚掉到地板下面。
//动画压缩

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class TestPlayable : MonoBehaviour
{
    public AnimationClip clip0;
    public AnimationClip clip1;
    private PlayableGraph m_Graph;
    private AnimationPlayableOutput m_Output;
    private AnimationMixerPlayable m_Mixer;

    [Range(0, 1)] public float weight;
    void Start()
    {
        // PlayableBehaviour
        m_Graph = PlayableGraph.Create("asdfas");
        m_Mixer = AnimationMixerPlayable.Create(m_Graph, 2);
        m_Output = AnimationPlayableOutput.Create(m_Graph, "Animation", GetComponent<Animator>());

        m_Output.SetSourcePlayable(m_Mixer);
        AnimationClipPlayable clipPlayable0 = AnimationClipPlayable.Create(m_Graph, clip0);
        AnimationClipPlayable clipPlayable1 = AnimationClipPlayable.Create(m_Graph, clip1);
        m_Graph.Connect(clipPlayable0, 0, m_Mixer, 0);
        m_Graph.Connect(clipPlayable1, 0, m_Mixer, 1);
        m_Mixer.SetInputWeight(0, 0.5f);
        m_Mixer.SetInputWeight(1, 0.5f);
        m_Graph.Play();
        // GameObject.Instantiate(gameObject, transform.parent);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,100,100),"coopy"))
        {
            for (int i = 0; i < 100; i++)
            {
                GameObject.Instantiate(gameObject, transform.parent);
            }
        }
    }

    void Update()
    {
        m_Mixer.SetInputWeight(0, weight);
        m_Mixer.SetInputWeight(1, 1 - weight);
    }
}