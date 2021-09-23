using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    public class SlotLine : MonoBehaviour
    {
        List<Slot> m_line = new List<Slot>();
        [SerializeField]
        RectTransform m_base = default;
        [SerializeField]
        RectTransform m_lineBottom = default;
        int m_crrentNum = 0;
        int m_slotSize = 150;
        float m_rotationTime = 1f;
        public float RotationTime
        {
            get => m_rotationTime;
            set
            {
                if (value > 0)
                {
                    m_rotationTime = value;
                }
            }
        }
        public bool Move { get; set; } = false;
        public void SetSlot(Slot slot)
        {
            var oneSlot = Instantiate(slot);
            oneSlot.SlotRect.transform.position = m_base.position;
            oneSlot.transform.SetParent(m_base);
            m_line.Add(oneSlot);
        }
        public void StartSlot()
        {
            if (Move)
            {
                return;
            }
            if (m_line.Count < 3)
            {
                Debug.Log("設定数未満");
                return;
            }
            for (int i = 0; i < 4; i++)
            {
                if (i + m_crrentNum >= m_line.Count)
                {
                    m_line[i + m_crrentNum - m_line.Count].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
                else
                {
                    m_line[i + m_crrentNum].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
            }
            StartCoroutine(MoveSlot());
        }
        IEnumerator MoveSlot()
        {
            float posY = 0;
            Move = true;
            while (Move)
            {
                posY -= m_slotSize * m_line.Count / RotationTime * Time.deltaTime;
                if (posY < -m_slotSize)
                {
                    posY = 0;
                    m_line[m_crrentNum].SlotRect.transform.position = m_base.position;
                    m_crrentNum++;
                    if (m_crrentNum >= m_line.Count)
                    {
                        m_crrentNum = 0;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (i + m_crrentNum >= m_line.Count)
                    {
                        m_line[i + m_crrentNum - m_line.Count].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i + new Vector3(0, posY, 0);
                    }
                    else
                    {
                        m_line[i + m_crrentNum].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i + new Vector3(0, posY, 0);
                    }
                }
                yield return null;
            }
            if (posY < -m_slotSize / 2)
            {
                m_line[m_crrentNum].SlotRect.transform.position = m_base.position;
                m_crrentNum++;
                if (m_crrentNum >= m_line.Count)
                {
                    m_crrentNum = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (i + m_crrentNum >= m_line.Count)
                {
                    m_line[i + m_crrentNum - m_line.Count].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
                else
                {
                    m_line[i + m_crrentNum].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
            }
        }
        public Slot GetSlot(int target)
        {
            if (Move || target > 2 || target < 0)
            {
                return null;
            }
            if (target + m_crrentNum >= m_line.Count)
            {
                return m_line[target + m_crrentNum - m_line.Count];
            }
            else
            {
                return m_line[target + m_crrentNum];
            }
        }
    }
}