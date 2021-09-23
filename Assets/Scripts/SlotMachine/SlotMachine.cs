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
        void Start()
        {
            m_slider.value = m_oneRotaionTime;
            m_speedText.text = $"一回転時間:{m_slider.value}秒";
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
            StopLeftLine();
            StopCenterLine();
            StopRightLine();
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
            if (Input.GetButtonDown("Jump"))
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
            m_leftLine.RotationTime = m_slider.value;
            m_centerLine.RotationTime = m_slider.value;
            m_rightLine.RotationTime = m_slider.value;
            m_leftLine.StartSlot();
            m_centerLine.StartSlot();
            m_rightLine.StartSlot();
        }
        public void SpeedChange()
        {
            m_leftLine.RotationTime = m_slider.value;
            m_centerLine.RotationTime = m_slider.value;
            m_rightLine.RotationTime = m_slider.value;
            m_speedText.text = $"一回転時間:{m_slider.value}秒";
        }
        public void StopLeftLine()
        {
            m_leftLine.Move = false;
        }
        public void StopCenterLine()
        {
            m_centerLine.Move = false;
        }
        public void StopRightLine()
        {
            m_rightLine.Move = false;
        }
    }
}