using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class BuildControl : MonoBehaviour
    {
        [SerializeField]
        Slot[] m_popSlot = default;
        [SerializeField]
        RectTransform[] m_slotPos = default;
        [SerializeField]
        RectTransform[] m_linePos = default;
        [SerializeField]
        RectTransform m_target = default;
        [SerializeField]
        RectTransform m_target2 = default;
        int m_targetNum = 0;
        int m_slotNum = 0;
        bool lineMode = false;
       
        public void StartSet()
        {
            if (BattleData.PopSlot != null && BattleData.PopSlot.Length > 0)
            {
                m_popSlot = BattleData.PopSlot;
            }
            for (int i = 0; i < m_popSlot.Length; i++)
            {
                int r = Random.Range(0, m_popSlot.Length);
                Slot a = m_popSlot[i];
                m_popSlot[i] = m_popSlot[r];
                m_popSlot[r] = a;
            }
            for (int i = 0; i < 3; i++)
            {
                var oneSlot = Instantiate(m_popSlot[i]);
                oneSlot.SlotRect.transform.position = m_slotPos[i].position;
                oneSlot.transform.SetParent(m_slotPos[i]);
            }
            m_target.transform.position = m_slotPos[m_targetNum].position;
            m_target2.transform.position = m_linePos[m_targetNum].position;
            m_target2.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        void Update()
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                var i = Input.GetAxisRaw("Horizontal");
                if (i > 0)
                {
                    m_targetNum = 2;
                    if (!lineMode)
                    {
                        m_target.transform.position = m_slotPos[m_targetNum].position;
                    }
                    else
                    {
                        m_target2.transform.position = m_linePos[m_targetNum].position;
                    }
                }
                else if (i < 0)
                {
                    m_targetNum = 0;
                    if (!lineMode)
                    {
                        m_target.transform.position = m_slotPos[m_targetNum].position;
                    }
                    else
                    {
                        m_target2.transform.position = m_linePos[m_targetNum].position;
                    }
                }
            }
            if (Input.GetButtonDown("Vertical"))
            {
                m_targetNum = 1;
                if (!lineMode)
                {
                    m_target.transform.position = m_slotPos[m_targetNum].position;
                }
                else
                {
                    m_target2.transform.position = m_linePos[m_targetNum].position;
                }
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (!lineMode)
                {
                    lineMode = true;
                    m_target2.gameObject.SetActive(true);
                    m_slotNum = m_targetNum;
                    m_targetNum = 0;
                }
                else
                {
                    SlotData.AddSlot(m_popSlot[m_slotNum], m_targetNum);
                    BattleManager.Instance.NextScene();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}