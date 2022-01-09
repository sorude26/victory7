using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace victory7
{ 
    public class TextChanger : MonoBehaviour
    {
        [Header("表示したいスコア")]
        [SerializeField] Text m_score = default;

        [Header("スコアが表示されるまでの秒数")]
        [SerializeField] float m_delayScore = 2f;

        readonly int  m_myScore = 200;
        int m_changeScore = default;
        int m_scoreText = default;
        float m_default = default;
        bool m_start = default; 

        void Start()
        {
            //FadeController.Instance.StartFadeIn(() => m_start = true);
            m_changeScore = 1 / m_myScore;
        }

        void Update()
        {
            if (m_default < m_delayScore)
            {
                m_default += Time.deltaTime;
                m_scoreText += m_changeScore;
                m_score.text = m_scoreText.ToString();
                Debug.Log("a");
            }

            if (!m_start) return;
            if (Input.GetButtonDown("Jump"))
            {
                ReturnTitle();
            }
        }
        public void ReturnTitle()
        {
            if (!m_start)
            {
                return;
            }
            m_start = false;
            FadeController.Instance.StartFadeOut(LoadTitle);
        }
        void LoadTitle()
        {
            SceneManager.LoadScene("αTitle");
        }
    }
}
