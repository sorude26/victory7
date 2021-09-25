using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class ViewText : MonoBehaviour
    {
        [SerializeField]
        Text m_text = default;
        [SerializeField]
        float m_viewTime = 1f;
        bool m_view = false;
        public void View(string text)
        {
            if (m_view) { return; }
            m_view = true;
            m_text.text = text;
            StartCoroutine(View());
        }
        public void View(string text,float time)
        {
            if (m_view) { return; }
            m_view = true;
            m_text.text = text;
            m_viewTime = time;
            StartCoroutine(View());
        }
        public void View(string text,Color color)
        {
            if (m_view) { return; }
            m_view = true;
            m_text.text = text;
            m_text.color = color;
            StartCoroutine(View());
        }
        public void View(string text, Color color,float time)
        {
            if (m_view) { return; }
            m_view = true;
            m_text.text = text;
            m_text.color = color;
            m_viewTime = time;
            StartCoroutine(View());
        }
        public void View(string text, Color color,int size)
        {
            if (m_view) { return; }
            m_view = true;
            m_text.text = text;
            m_text.color = color;
            m_text.fontSize = size;
            StartCoroutine(View());
        }
        public void View(string text, Color color, int size,float time)
        {
            if (m_view) { return; }
            m_view = true;
            m_text.text = text;
            m_text.color = color;
            m_text.fontSize = size;
            m_viewTime = time;
            StartCoroutine(View());
        }
        IEnumerator View()
        {
            yield return new WaitForSeconds(m_viewTime);
            Destroy(gameObject);
        }
    }
}