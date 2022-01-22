using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

namespace victory7
{ 
    public class TextChanger : MonoBehaviour
    {
        Text m_scoreText = default;

        [Header("表示したいスコア名")]
        [SerializeField] string m_scoreName = default;

        [Header("スコアが表示されるまでの秒数")]
        [SerializeField] float m_scoreChangeInterval = 1f;

        [Header("受け取りたい変数名")]
        [SerializeField] string m_takeOverName = default;

        [Header("変わる数")]
        [SerializeField] int m_changeScore = default;

        int m_myScore = default;//変数を受け取り反映する変数

        const int m_maxScore = 999999999;

        readonly int m_test00 = default;

        //float m_scoreText = default;

        static int m_mapData;
        int m_victoryCount;
        int m_sevenSlot;

        
        bool m_start = default;

        bool m_jump = default;

        void Start()
        {
            m_scoreText = GetComponent<Text>();

            m_mapData = MapData.ClearStageCount;

            m_victoryCount = MapData.ClearStageCount;

            m_sevenSlot = SlotData.SevenSlotData.Count;

            //FadeController.Instance.StartFadeIn(() => m_start = true);

            if ((nameof(m_mapData)) == m_takeOverName)
            {
                m_myScore = m_mapData; 
            }   
            else if((nameof(m_victoryCount)) == m_takeOverName)
            {
                m_myScore = m_victoryCount;
            }
            else if((nameof(m_sevenSlot)) == m_takeOverName)
            {
                m_myScore = m_sevenSlot;
            }
            else if((nameof(m_test00)) == m_takeOverName)
            {
                m_myScore = m_test00;
            }
            Debug.Log(m_myScore);
        }

        async void Update()
        {
            if (!m_jump && Input.GetButtonDown("Jump"))
            {
                AddScore();
                await Task.Delay(1000);
                m_jump = true;
            }
            if(m_jump && Input.GetButtonDown("Jump"))
            {
                ReturnTitle();
            }
        }

        public void AddScore()
        {
            m_scoreText.DOText(m_scoreName + "     " + m_myScore.ToString(),m_scoreChangeInterval);
        }

        public void ReturnTitle()
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
