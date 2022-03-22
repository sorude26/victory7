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

        [Header("スコアが表示されるまでの秒数")]
        [SerializeField] float m_scoreChangeInterval = 2f;

        [Header("受け取りたい変数の番号")]
        [SerializeField] int m_takeNum = default;

        [SerializeField]
        [Header("戦闘勝利回数を参照するスコア計算用の数値")]
        int m_scoreCalculation = 100;

        [SerializeField]
        [Header("クリアマップ数を参照するスコア計算用の数値")]
        int m_scoreCalculationForClearMapCount = 2500;

        int m_myScore = default;//変数を受け取り反映する変数

        int m_ClearStageCountData;
        int m_victoryCount;
        int m_sevenSlot;

        
        bool m_start = false;

        public int Score => m_handOverScore;

        int m_handOverScore;

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
                if(m_ClearStageCountData == 0)
                {
                    m_myScore = m_victoryCount * m_scoreCalculation;
                }
                else
                {
                    m_myScore = (m_victoryCount * m_scoreCalculation) + ((m_ClearStageCountData - 1) * m_scoreCalculationForClearMapCount) + 10000;
                }
                m_handOverScore = m_myScore;
            }

            AddScore();
        }

        void Update()
        {
            if(Input.GetButtonDown("Jump"))
            {
                ReturnTitle();
            }
        }

        void AddScore()
        {
            m_scoreText.DOText(m_myScore.ToString(),m_scoreChangeInterval);
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
    }
}
