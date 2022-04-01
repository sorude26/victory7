using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventDialog : MonoBehaviour
{
    [SerializeField]
    GameObject m_base = default;
    [SerializeField]
    Transform m_cursor = default;
    [SerializeField]
    Transform[] m_cursorPos = default;
    [SerializeField]
    Image m_image = default;
    [SerializeField]
    Text m_messageText = default;
    SelectType m_currentSelect = default;
    public bool IsOpen { get; private set; }
    public string SetMessage 
    {
        set
        {
            m_messageText.text = value;
        } 
    }
    private enum SelectType
    {
        Cancel,
        Decision,
    }
    private void Start()
    {
        m_base.SetActive(false);
    }
    public void SetImage(Sprite sprite)
    {
        m_image.sprite = sprite;
    }
    public void OpenDialog()
    {
        IsOpen = true;
        m_base.SetActive(true);
        m_currentSelect = SelectType.Cancel;
    }
    public void CloseDialog()
    {
        IsOpen = false;
        m_cursor.transform.localPosition = m_cursorPos[(int)SelectType.Cancel].localPosition;
        m_base.SetActive(false);
    }
    public bool SelectDecision()
    {
        switch (m_currentSelect)
        {
            case SelectType.Decision:
                CloseDialog();
                return true;
            case SelectType.Cancel:
                CloseDialog();
                return false;
            default: return false;
        }
    }
    public void ChangeSelect(int dir)
    {
        int current = (int)m_currentSelect;
        int max = Enum.GetValues(typeof(SelectType)).Length;
        if (dir > 0)
        {
            current++;
            if (current >= max)
            {
                current = 0;
            }
        }
        else if (dir < 0)
        {
            current--;
            if (current < 0)
            {
                current = max - 1;
            }
        }
        m_currentSelect = (SelectType)current;
        m_cursor.transform.localPosition = m_cursorPos[current].localPosition;
        SoundManager.Play(SEType.Choice);
    }
}
