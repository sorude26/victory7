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

        [Header("表示したいスコア名")]
        [SerializeField] string m_scoreName = default;

        [Header("スコアが表示されるまでの秒数")]
        [SerializeField] int m_delayScore = 1;

        [Header("受け取りたい変数名")]
        [SerializeField] string m_takeOverName = default;

        int m_myScore = default;//変数を受け取り反映する変数

        readonly int m_test00 = default;//これらの変数に入力された値を入れる
        readonly int m_test01 = default;
        readonly int m_test02 = default;
        readonly int m_test03 = default;

        int m_changeScore = default;
        int m_scoreText = default;
        float m_default = default;
        bool m_start = default; 

        void Start()
        {
            //FadeController.Instance.StartFadeIn(() => m_start = true);

            if(m_test00.ToString() == m_takeOverName)
            {
                m_myScore = m_test00;
            }
            else if(m_test01.ToString() == m_takeOverName)
            {
                m_myScore = m_test01;
            }
            else if(m_test02.ToString() == m_takeOverName)
            {
                m_myScore = m_test02;
            }
            else if(m_test03.ToString() == m_takeOverName)
            {
                m_myScore = m_test03;
            }

            m_changeScore = m_myScore / m_delayScore;
        }

        void Update()
        {
            if (m_default < m_delayScore)
            {
                m_default += Time.deltaTime;
                m_scoreText += m_changeScore;
                m_score.text = m_scoreName + "      " + m_scoreText.ToString();
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
