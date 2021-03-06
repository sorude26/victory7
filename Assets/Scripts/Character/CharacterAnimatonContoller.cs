using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterAnimatonContoller : MonoBehaviour
{
    [SerializeField]
    Animator m_animator = default;
    string m_current = default;
    public Action<string> OnPlayEnd = default;
    public void PlayAction(string action = "",float m_changeAnimeTime = 0.1f)
    {
        if (!gameObject.activeSelf) { return; }
        m_current = action;
        m_animator.CrossFadeInFixedTime(action, m_changeAnimeTime);
    }
    public void SetAction(string action)
    {
        if (!gameObject.activeSelf) { return; }
        m_current = action;
    }
    public void ChangeSpeed(float speed = 1f)
    {
        if (!gameObject.activeSelf) { return; }
        m_animator.SetFloat("PlaySpeed", speed);
    }
    public void SetBool(string target,bool set = true)
    {
        if (!gameObject.activeSelf) { return; }
        m_animator.SetBool(target, set);
    }
    void PlayEnd() 
    {
        OnPlayEnd?.Invoke(m_current); 
    }
}
