using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class SlotMachine : MonoBehaviour
    {
        [Header("一回転に掛かる時間")]
        [SerializeField]
        float m_oneRotaionTime = 0.85f;
        [SerializeField]
        Slot[] m_testSlotL = default;
        [SerializeField]
        Slot[] m_testSlotC = default;
        [SerializeField]
        Slot[] m_testSlotR = default;
        [SerializeField]
        SlotLine m_leftLine = default;
        [SerializeField]
        SlotLine m_centerLine = default;
        [SerializeField]
        SlotLine m_rightLine = default;
        [SerializeField]
        Slider m_slider = default;
        [SerializeField]
        Text m_speedText = default;
        bool m_chack = false;
        void Start()
        {
            StartSet();
        }
        public void StartSet()
        {
            if (m_slider)
            {
                m_slider.value = m_oneRotaionTime;
            }
            if (m_speedText)
            {
                m_speedText.text = $"一回転時間:{m_slider.value}秒";
            }
            foreach (var item in m_testSlotL)
            {
                m_leftLine.SetSlot(item);
            }
            foreach (var item in m_testSlotC)
            {
                m_centerLine.SetSlot(item);
            }
            foreach (var item in m_testSlotR)
            {
                m_rightLine.SetSlot(item);
            }
            StartSlot();
            m_leftLine.Move = false;
            m_centerLine.Move = false;
            m_rightLine.Move = false;
        }
        private void Update()
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                var i = Input.GetAxisRaw("Horizontal");
                if (i > 0)
                {
                    StopRightLine();
                }
                else if (i < 0)
                {
                    StopLeftLine();
                }
                else
                {
                    StopRightLine();
                    StopLeftLine();
                }
            }
            if (Input.GetButtonDown("Vertical"))
            {
                StopCenterLine();
            }
            if (Input.GetButtonDown("Jump") && !m_chack)
            {
                StartSlot();
            }
        }
        public void StartSlot()
        {
            if (m_leftLine.Move || m_centerLine.Move || m_rightLine.Move)
            {
                return;
            }
            if (m_slider)
            {
                m_leftLine.RotationTime = m_slider.value;
                m_centerLine.RotationTime = m_slider.value;
                m_rightLine.RotationTime = m_slider.value;
            }
            else
            {
                m_leftLine.RotationTime = m_oneRotaionTime;
                m_centerLine.RotationTime = m_oneRotaionTime;
                m_rightLine.RotationTime = m_oneRotaionTime;
            }
            m_leftLine.StartSlot();
            m_centerLine.StartSlot();
            m_rightLine.StartSlot();
        }
        public void SpeedChange()
        {
            m_leftLine.RotationTime = m_slider.value;
            m_centerLine.RotationTime = m_slider.value;
            m_rightLine.RotationTime = m_slider.value; 
            if (m_speedText)
            {
                m_speedText.text = $"一回転時間:{m_slider.value}秒";
            }
        }
        public void StopLeftLine()
        {
            if (m_chack)
            {
                return;
            }
            m_leftLine.Move = false;
            CheckSlot();
        }
        public void StopCenterLine()
        {
            if (m_chack)
            {
                return;
            }
            m_centerLine.Move = false;
            CheckSlot();
        }
        public void StopRightLine()
        {
            if (m_chack)
            {
                return;
            }
            m_rightLine.Move = false;
            CheckSlot();
        }
        public void CheckSlot()
        {
            if (m_leftLine.Move || m_centerLine.Move || m_rightLine.Move)
            {
                return;
            }
            m_chack = true;
            StartCoroutine(Check());
        }
        IEnumerator Check()
        {
            yield return new WaitForSeconds(0.1f);
            if (CheckLine(1))
            {
                m_leftLine.GetSlot(1).PlayEffect();
            }
            if (CheckLine(2))
            {
                m_leftLine.GetSlot(2).PlayEffect();
            }
            if (CheckLine(0))
            {
                m_leftLine.GetSlot(0).PlayEffect();
            }
            if (CheckDiagonalLineDown())
            {
                m_leftLine.GetSlot(2).PlayEffect();
            }
            if (CheckDiagonalLineUP())
            {
                m_leftLine.GetSlot(0).PlayEffect();
            }
            yield return new WaitForSeconds(0.5f);
            StartSlot();
            yield return new WaitForSeconds(0.5f);
            m_chack = false;
        }
        bool CheckLine(int lineNum)
        {
            var left = m_leftLine.GetSlot(lineNum);
            var center = m_centerLine.GetSlot(lineNum);
            var right = m_rightLine.GetSlot(lineNum);
            if (!left || !center || !right)
            {
                Debug.Log("Null!!");
                return false;
            }
            if (left.Type == center.Type && left.TestDebagEffect == center.TestDebagEffect)
            {
                if (left.Type == right.Type && left.TestDebagEffect == right.TestDebagEffect)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool CheckDiagonalLineUP()
        {
            var left = m_leftLine.GetSlot(0);
            var center = m_centerLine.GetSlot(1);
            var right = m_rightLine.GetSlot(2);
            if (!left || !center || !right)
            {
                Debug.Log("Null!!");
                return false;
            }
            if (left.Type == center.Type && left.TestDebagEffect == center.TestDebagEffect)
            {
                if (left.Type == right.Type && left.TestDebagEffect == right.TestDebagEffect)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        bool CheckDiagonalLineDown()
        {
            var left = m_leftLine.GetSlot(2);
            var center = m_centerLine.GetSlot(1);
            var right = m_rightLine.GetSlot(0);
            if (!left || !center || !right)
            {
                Debug.Log("Null!!");
                return false;
            }
            if (left.Type == center.Type && left.TestDebagEffect == center.TestDebagEffect)
            {
                if (left.Type == right.Type && left.TestDebagEffect == right.TestDebagEffect)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}