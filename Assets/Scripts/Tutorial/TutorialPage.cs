using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPage : MonoBehaviour
{
    [SerializeField]
    TutorialType m_tutorialType = TutorialType.Map;
    [SerializeField]
    private GameObject[] m_pages = default;

    private int m_currentPage = 0;

    public TutorialType Type { get => m_tutorialType; }
    public bool IsEnd { get; private set; }
    private void Start()
    {
        ClosePage();
    }
    public void NextPage()
    {
        if (m_currentPage < m_pages.Length - 1)
        {
            m_currentPage++;
            OpenPage();
        }
        else
        {
            ClosePage();
            IsEnd = true;
        }
    }
    public void ReturnPage()
    {
        if (m_currentPage > 0)
        {
            m_currentPage--;
            OpenPage();
        }
    }
    public void OpenPage()
    {
        ClosePage();
        m_pages[m_currentPage].gameObject.SetActive(true);
    }
    public void ClosePage()
    {
        foreach (var page in m_pages)
        {
            page.gameObject.SetActive(false);
        }
    }
}
