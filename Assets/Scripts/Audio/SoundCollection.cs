using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SoundCollection
{
    [SerializeField] private List<AudioClip> m_clips;
    [SerializeField] private bool m_randomizePitch;
    [SerializeField] private float m_minPitch;
    [SerializeField] private float m_maxPitch;

    public List<AudioClip> Get() { return m_clips; }
    public AudioClip GetRandom()
    {
        if (m_clips == null) return null;
        int index = UnityEngine.Random.Range(0, m_clips.Count - 1);
        return m_clips[index];
    }

    public void PlayRandom(AudioSource s)
    {
        float backup = s.pitch;
        if(m_randomizePitch)
        {
            s.pitch = UnityEngine.Random.Range(m_minPitch, m_maxPitch);
        }
        s.PlayOneShot(GetRandom());
        s.pitch = backup;
    }
}
