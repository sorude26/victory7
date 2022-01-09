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
        [SerializeField]
        float m_slotSize = 150;
        [SerializeField]
        float m_slotScale = 1;
        float m_rotationTime = 1f;
        float m_slotSpeed = 1f;
        bool m_start = false;
        public int CrrentNum { get; private set; } = 0;
        public SEType StopSE { get; set; } = SEType.StopSpin;
        public bool SlotMove { get; private set; }
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
        public float SlotSpeed
        {
            get => m_slotSpeed;
            set
            {
                if (value > 0)
                {
                    m_slotSpeed = value;
                }
            }
        }
        public bool Move { get; set; } = false;
        public void SetSlot(Slot slot)
        {
            var oneSlot = Instantiate(slot);
            oneSlot.SlotRect.transform.position = m_base.position;
            oneSlot.transform.localScale = Vector3.one * m_slotScale;
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
                if (i + CrrentNum >= m_line.Count)
                {
                    m_line[i + CrrentNum - m_line.Count].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
                else
                {
                    m_line[i + CrrentNum].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
            }
            StartCoroutine(MoveSlot());
        }
        IEnumerator MoveSlot()
        {
            float posY = 0;
            Move = true;
            SlotMove = true;
            while (Move)
            {
                posY -= m_slotSize * m_line.Count * m_slotSpeed / RotationTime * Time.deltaTime;
                if (posY < -m_slotSize)
                {
                    posY = 0;
                    m_line[CrrentNum].SlotRect.transform.position = m_base.position;
                    CrrentNum++;
                    if (CrrentNum >= m_line.Count)
                    {
                        CrrentNum = 0;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (i + CrrentNum >= m_line.Count)
                    {
                        m_line[i + CrrentNum - m_line.Count].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i + new Vector3(0, posY, 0);
                    }
                    else
                    {
                        m_line[i + CrrentNum].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i + new Vector3(0, posY, 0);
                    }
                }
                yield return null;
            }
            if (posY < -m_slotSize / 2)
            {
                m_line[CrrentNum].SlotRect.transform.position = m_base.position;
                CrrentNum++;
                if (CrrentNum >= m_line.Count)
                {
                    CrrentNum = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (i + CrrentNum >= m_line.Count)
                {
                    m_line[i + CrrentNum - m_line.Count].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
                else
                {
                    m_line[i + CrrentNum].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
            }
            SlotMove = false;
            if (m_start)
            {
                SoundManager.Play(StopSE);
            }
            else
            {
                m_start = true;
            }
        }
        public void TargetStop(int target)
        {
            StopAllCoroutines();
            CrrentNum = target;
            for (int i = 0; i < 4; i++)
            {
                if (i + CrrentNum >= m_line.Count)
                {
                    m_line[i + CrrentNum - m_line.Count].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
                else
                {
                    m_line[i + CrrentNum].transform.position = m_lineBottom.position + Vector3.up * m_slotSize * i;
                }
            }
            Move = false;
            SlotMove = false;
            SoundManager.Play(StopSE);
        }
        public Slot GetSlot(int target)
        {
            if (Move || target > 2 || target < 0)
            {
                return null;
            }
            if (target + CrrentNum >= m_line.Count)
            {
                return m_line[target + CrrentNum - m_line.Count];
            }
            else
            {
                return m_line[target + CrrentNum];
            }
        }
    }
}