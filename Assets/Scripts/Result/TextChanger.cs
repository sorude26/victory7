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
        Text m_scoreText;

        [Header("表示したいスコア名")]
        [SerializeField] string m_scoreName = default;

        [Header("スコアが表示されるまでの秒数")]
        [SerializeField] float m_scoreChangeInterval = 2f;

        [Header("受け取りたい変数の番号")]
        [SerializeField] int m_takeNum = default;

        int m_myScore = default;//変数を受け取り反映する変数

        readonly int m_test00 = default;

        int m_ClearStageCountData;
        int m_victoryCount;
        int m_sevenSlot;

        
        bool m_start = default;

        bool m_flag = default;

        bool m_jump = default;


        private void Awake()
        {
            m_scoreText = GetComponent<Text>();

            m_ClearStageCountData = MapData.ClearStageCount;

            m_victoryCount = MapData.BattleCount;

            m_sevenSlot = SlotData.SevenSlotCount();

            FadeController.Instance.StartFadeIn(() => m_start = true);
        }

        private void Start()
        {
            if (m_takeNum == 1)
            {
                m_myScore = m_ClearStageCountData; 
            }   
            else if(m_takeNum == 2)
            {
                m_myScore = m_victoryCount;
            }
            else if(m_takeNum == 3)
            {
                m_myScore = m_sevenSlot;
            }
            else if(m_takeNum == 4)
            {
                m_myScore = m_test00;
            }
        }

        void Update()
        {
            if (!m_jump && Input.GetButtonDown("Jump"))
            {
                AddScore();
                StartCoroutine(WaitInput());
                m_jump = true;
            }
            if(m_flag && Input.GetButtonDown("Jump"))
            {
                ReturnTitle();
            }
        }

        void AddScore()
        {
            m_scoreText.DOText(m_scoreName + "     " + m_myScore.ToString(),m_scoreChangeInterval);
        }

        void ReturnTitle()
        {
            if (!m_start) return;
            m_start = false;
            FadeController.Instance.StartFadeOut(LoadTitle);
        }

        void LoadTitle()
        {
            SceneManager.LoadScene("αTitle");
        }

        IEnumerator WaitInput()
        {
            yield return new WaitForSeconds(1);
            m_flag = true;
        }
    }
}
