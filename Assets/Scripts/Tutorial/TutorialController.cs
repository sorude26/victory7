using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialType
{
    Map,
    Battle,
    Build,
}
public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance {get; private set;}
    [SerializeField]
    private TutorialPage[] m_tutorialPages = default;

    private Dictionary<TutorialType, TutorialPage> m_tutorialDic = default;
    private bool[] m_tutorialIsEnds = default;
    private TutorialType m_currentTutorial = default;
    public bool IsActive { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            m_tutorialDic = new Dictionary<TutorialType, TutorialPage>();
            for (int i = 0; i < m_tutorialPages.Length; i++)
            {
                m_tutorialDic.Add((TutorialType)i, m_tutorialPages[i]);
            }
            m_tutorialIsEnds = new bool[m_tutorialPages.Length];
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayTutorial(TutorialType type, Action endAction)
    {
        if (Instance == null) { return; }
        if (IsActive) { return; }
        m_currentTutorial = type;
        if (!m_tutorialIsEnds[(int)type])
        {
            IsActive = true;
            SoundManager.Play(SEType.Deployment);
            StartCoroutine(ActiveTutorial(endAction));
        }
        else
        {
            endAction?.Invoke();
        }
    }
    private IEnumerator ActiveTutorial(Action endAction)
    {
        m_tutorialDic[m_currentTutorial].OpenPage();
        while (true)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                var i = Input.GetAxisRaw("Horizontal");
                if (i > 0)
                {
                    m_tutorialDic[m_currentTutorial].NextPage();
                    if (m_tutorialDic[m_currentTutorial].IsEnd)
                    {
                        break;
                    }
                }
                else if (i < 0)
                {
                    m_tutorialDic[m_currentTutorial].ReturnPage();
                }
            }
            yield return null;
        }
        m_tutorialIsEnds[(int)m_currentTutorial] = true;
        IsActive = false;
        endAction?.Invoke();
    }
}
