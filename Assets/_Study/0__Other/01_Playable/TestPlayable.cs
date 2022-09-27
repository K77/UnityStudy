//foot ik 可以防止脚掉到地板下面。
//动画压缩

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class TestPlayable : MonoBehaviour
{
    public Slider _SliderHeight;
    public Slider _SliderSpeed;
    public AnimationClip clipIdleH;
    public AnimationClip clipIdleM;
    public AnimationClip clipIdleL;
    public AnimationClip clipWalkH;
    public AnimationClip clipWalkM;
    public AnimationClip clipWalkL;
    public AnimationClip clipCrabH;
    public AnimationClip clipCrabM;
    public AnimationClip clipCrabL;
    
    private PlayableGraph m_Graph;
    private AnimationPlayableOutput m_Output;
    private AnimationMixerPlayable m_MixerIdle;
    private AnimationMixerPlayable m_MixerWalk;
    private AnimationMixerPlayable m_MixerAll;

    [Range(0, 1)] public float weight;
    void Start()
    {
        // PlayableBehaviour
        m_Graph = PlayableGraph.Create("Graph");
        m_MixerIdle = AnimationMixerPlayable.Create(m_Graph, 2);
        m_MixerWalk = AnimationMixerPlayable.Create(m_Graph, 2);
        m_MixerAll = AnimationMixerPlayable.Create(m_Graph, 2);
        m_Output = AnimationPlayableOutput.Create(m_Graph, "Animation", GetComponent<Animator>());

        AnimationClipPlayable clipPlayable0 = AnimationClipPlayable.Create(m_Graph, clipIdleH);
        AnimationClipPlayable clipPlayable1 = AnimationClipPlayable.Create(m_Graph, clipIdleM);
        AnimationClipPlayable clipPlayable2 = AnimationClipPlayable.Create(m_Graph, clipWalkH);
        AnimationClipPlayable clipPlayable3 = AnimationClipPlayable.Create(m_Graph, clipWalkM);
        
        // m_ParentMixerPlayable.AddInput(m_IdleMixerPlayable, 0, 1);
        // m_ParentMixerPlayable.AddInput(m_MoveMixerPlayable, 0, 0);
        
        m_Graph.Connect(clipPlayable0, 0, m_MixerIdle, 0);
        m_Graph.Connect(clipPlayable1, 0, m_MixerIdle, 1);
        m_Graph.Connect(clipPlayable2, 0, m_MixerWalk, 0);
        m_Graph.Connect(clipPlayable3, 0, m_MixerWalk, 1);
        
        m_MixerAll.AddInput(m_MixerIdle,0,0);
        m_MixerAll.AddInput(m_MixerWalk,0,1);

        // m_Mixer.SetInputWeight(0, 0.5f);
        // m_Mixer.SetInputWeight(1, 0.5f);
        // clipIdleH.isLooping = true;
        // GameObject.Instantiate(gameObject, transform.parent);

        _SliderHeight.value = 1;
        _SliderHeight.onValueChanged.AddListener((value) =>
        {
            changeBlend();
        });
        
        _SliderSpeed.onValueChanged.AddListener((value) =>
        {
            changeBlend();
            Debug.Log(_SliderSpeed.value);
        });
        m_MixerAll.SetInputWeight(m_MixerIdle,0);
        m_MixerAll.SetInputWeight(m_MixerWalk,1);
        m_MixerWalk.SetInputWeight(0,1);
        m_MixerWalk.SetInputWeight(1,0);
        m_Output.SetSourcePlayable(m_MixerAll);
        m_Graph.Play();
    }

    void changeBlend()
    {
        m_MixerIdle.SetInputWeight(0, _SliderHeight.value);
        m_MixerIdle.SetInputWeight(1, 1 - _SliderHeight.value);
        m_MixerWalk.SetInputWeight(0, _SliderHeight.value);
        m_MixerWalk.SetInputWeight(1, 1 - _SliderHeight.value);
        
        m_MixerAll.SetInputWeight(m_MixerIdle,1- _SliderSpeed.value);
        m_MixerAll.SetInputWeight(m_MixerWalk,_SliderSpeed.value);
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
        // m_Mixer.SetInputWeight(0, weight);
        // m_Mixer.SetInputWeight(1, 1 - weight);
    }
}