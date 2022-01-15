using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace victory7
{ 
    public class TextChanger : MonoBehaviour
    {
        [Header("表示したいスコア")]
        [SerializeField] Text m_scoreText = default;

        [Header("表示したいスコア名")]
        [SerializeField] string m_scoreName = default;

        [Header("スコアが表示されるまでの秒数")]
        [SerializeField] float m_scoreChangeInterval = 1f;

        [Header("受け取りたい変数名")]
        [SerializeField] string m_takeOverName = default;

        int m_myScore = default;//変数を受け取り反映する変数

        const int m_maxScore = 999999999;

        readonly int m_test00 = 100;//これらの変数に入力された値を入れる
        readonly int m_test01 = default;
        readonly int m_test02 = default;
        readonly int m_test03 = default;

        //float m_scoreText = default;
        bool m_start = default; 

        void Start()
        {
            //FadeController.Instance.StartFadeIn(() => m_start = true);

            if (m_test00.ToString() == m_takeOverName)
            {
                m_myScore = m_test00;
                Debug.Log("z"); 
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

            AddScore(m_myScore);
        }

        void Update()
        {
            if (m_start && Input.GetButtonDown("Jump"))
            {
                ReturnTitle();
            }
        }

        public void AddScore(int score)
        {
            int tempScore = m_myScore;
            m_myScore = Mathf.Min(m_myScore + score, m_maxScore);

            if(tempScore != m_maxScore)
            {
                DOTween.To(() => tempScore,
                    x => tempScore = x,
                    m_myScore,
                    m_scoreChangeInterval)
                    .OnUpdate(() => m_scoreText.text = tempScore.ToString(m_scoreName + "     " + "00000"))
                    .OnComplete(() => m_scoreText.text = m_myScore.ToString(m_scoreName + "     " + "00000"));
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
