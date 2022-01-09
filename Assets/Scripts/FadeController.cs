using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance { get; private set; }
    [SerializeField] float m_fadeSpeed = 1f;
    [SerializeField] Image m_fadePanel = default;
    Color m_fadePanelColor;
    public static bool FadeNow { get; private set; }
    private void Awake()
    {
        Instance = this;
        m_fadePanel.gameObject.SetActive(true);
        m_fadePanelColor = m_fadePanel.color;
    }
    public void StartFadeIn()
    {
        if (FadeNow)
        {
            return;
        }
        StartCoroutine(FadeIn());
    }
    public void StartFadeIn(Action fadeInAction)
    {
        if (FadeNow)
        {
            return;
        }
        StartCoroutine(FadeIn(fadeInAction));
    }
    public void StartFadeOut(Action fadeOutAction)
    {
        if (FadeNow)
        {
            return;
        }
        StartCoroutine(FadeOut(fadeOutAction));
    }
    public void StartFadeOutIn(Action fadeInAction)
    {
        if (FadeNow)
        {
            return;
        }
        StartCoroutine(FadeOutIn(fadeInAction));
    }
    public void StartFadeOutIn(Action fadeOutAction, Action fadeInAction)
    {
        if (FadeNow)
        {
            return;
        }
        StartCoroutine(FadeOutIn(fadeOutAction, fadeInAction));
    }
    IEnumerator FadeIn(Action action)
    {
        FadeNow = true;
        yield return FadeIn();
        action.Invoke();
        FadeNow = false;
    }
    IEnumerator FadeOut(Action action)
    {
        FadeNow = true;
        yield return FadeOut();
        action.Invoke();
        FadeNow = false;
    }
    IEnumerator FadeOutIn(Action action)
    {
        FadeNow = true;
        yield return FadeOut();
        yield return FadeIn();
        action.Invoke();
        FadeNow = false;
    }
    IEnumerator FadeOutIn(Action fadeOutAction, Action fadeInAction)
    {
        FadeNow = true;
        yield return FadeOut();
        fadeOutAction.Invoke();
        yield return FadeIn();
        fadeInAction.Invoke();
        FadeNow = false;
    }
    IEnumerator FadeIn()
    {
        m_fadePanel.gameObject.SetActive(true);
        float a = 1;
        while (a > 0)
        {
            a -= m_fadeSpeed * Time.deltaTime;
            if (a <= 0)
            {
                a = 0;
            }
            m_fadePanel.color = m_fadePanelColor * new Color(1, 1, 1, a);
            yield return null;
        }
        m_fadePanel.gameObject.SetActive(false);
    }
    IEnumerator FadeOut()
    {
        m_fadePanel.gameObject.SetActive(true);
        float a = 0;
        while (a < 1f)
        {
            a += m_fadeSpeed * Time.deltaTime;
            if (a >= 1f)
            {
                a = 1f;
            }
            m_fadePanel.color = m_fadePanelColor * new Color(1, 1, 1, a);
            yield return null;
        }
    }
}