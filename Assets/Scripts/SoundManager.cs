using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SEType
{
    StopSpin,
    StopSpin7,
    StartSpin,
    PaylineAttack,
    PaylineHeel,
    PaylineGuard,
    PaylineCharge,
    Jackpot,
    Attack,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    static SoundManager instance = default;

    [SerializeField]
    AudioClip[] m_seClips = default;

    AudioSource m_audio = default;
    Dictionary<SEType, AudioClip> m_seDic = default;
    private void Awake()
    {
        instance = this;
        m_audio = GetComponent<AudioSource>();
        m_seDic = new Dictionary<SEType, AudioClip>();
        for (int i = 0; i < m_seClips.Length; i++)
        {
            m_seDic.Add((SEType)i, m_seClips[i]);
        }
    }

    public static void Play(SEType type)
    {
        if (instance)
        {
            instance.m_audio.PlayOneShot(instance.m_seDic[type]);
            //Debug.Log(type);
        }
    }
    public static void PlayBGM()
    {
        instance.m_audio.Play();
    }
}
