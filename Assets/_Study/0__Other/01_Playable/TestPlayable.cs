using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class TestPlayable : MonoBehaviour
{
    public AnimationClip clip0;
    public AnimationClip clip1;
    private PlayableGraph m_Graph;
    private AnimationPlayableOutput m_Output;
    private AnimationMixerPlayable m_Mixer;

    public float weight;
    void Start()
    {
        m_Mixer = AnimationMixerPlayable.Create(m_Graph, 2);
        m_Output.SetSourcePlayable(m_Mixer);
        AnimationClipPlayable clipPlayable0 = AnimationClipPlayable.Create(m_Graph, clip0);
        AnimationClipPlayable clipPlayable1 = AnimationClipPlayable.Create(m_Graph, clip1);
        m_Graph.Connect(clipPlayable0, 0, m_Mixer, 0);
        m_Graph.Connect(clipPlayable1, 0, m_Mixer, 1);
        m_Mixer.SetInputWeight(0, 1);
        m_Mixer.SetInputWeight(1, 0);
    }

    void Update()
    {
        m_Mixer.SetInputWeight(0, weight);
        m_Mixer.SetInputWeight(1, 1 - weight);
    }
}