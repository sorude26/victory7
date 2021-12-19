using System;
using System.Collections.Generic;
using UnityEngine;

public class LineControl : MonoBehaviour
{
    [SerializeField]
    Animator[] m_lines = new Animator[5];
    bool[] m_view = new bool[5];
    public void PlayLine(int number)
    {
        m_view[number] = true;
        m_lines[number].Play("alignSlots");
        SoundManager.Play(SEType.Jackpot);
    }
    public void EndView()
    {
        for (int i = 0; i < m_lines.Length; i++)
        {
            if (m_view[i])
            {
                m_view[i] = false;
                //m_lines[i].Play("close");
            }
        }
    }
}
