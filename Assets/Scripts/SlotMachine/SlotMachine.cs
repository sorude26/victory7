﻿using System;
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
        [SerializeField]
        bool m_sevenSlot = false;
        [SerializeField]
        float m_stopSpan = 0.5f;
        public bool Chack { get; private set; } = false;
        public bool Stop { get; private set; } = false;
        public Action StopSlot;

        //void Start()
        //{
        //    StartSet();
        //}
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
            if (m_sevenSlot)
            {
                foreach (var item in m_testSlotL)
                {
                    m_leftLine.SetSlot(item);
                }
                foreach (var item in m_testSlotL)
                {
                    m_centerLine.SetSlot(item);
                }
                foreach (var item in m_testSlotL)
                {
                    m_rightLine.SetSlot(item);
                }
            }
            else
            {
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
            if (Input.GetButtonDown("Jump") && !Chack)
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
            Stop = false;
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
        public void StopAll()
        {
            m_leftLine.Move = false;
            m_centerLine.Move = false;
            m_rightLine.Move = false;
            StartCoroutine(StopAllSlot());
        }
        IEnumerator StopAllSlot()
        {
            while (m_leftLine.SlotMove || m_centerLine.SlotMove || m_rightLine.SlotMove)
            {
                yield return null;
            }
            Stop = true;
        }
        public void StopLeftLine()
        {
            if (Chack || Stop)
            {
                return;
            }
            if (m_sevenSlot)
            {
                StartCoroutine(SpanStop1());
                return;
            }
            m_leftLine.Move = false;
            CheckSlot();
        }
        public void StopCenterLine()
        {
            if (Chack || Stop)
            {
                return;
            }
            if (m_sevenSlot)
            {
                StartCoroutine(SpanStop3());
                return;
            }
            m_centerLine.Move = false;
            CheckSlot();
        }
        public void StopRightLine()
        {
            if (Chack || Stop)
            {
                return;
            }
            if (m_sevenSlot)
            {
                StartCoroutine(SpanStop2());
                return;
            }
            m_rightLine.Move = false;
            CheckSlot();
        }
        IEnumerator SpanStop1()
        {
            Chack = true;
            yield return new WaitForSeconds(m_stopSpan);
            m_leftLine.Move = false;
            yield return new WaitForSeconds(m_stopSpan);
            m_centerLine.TargetStop(m_leftLine.CrrentNum);
            yield return new WaitForSeconds(m_stopSpan);
            m_rightLine.TargetStop(m_leftLine.CrrentNum);
            CheckSlot();
        }
        IEnumerator SpanStop2()
        {
            Chack = true;
            yield return new WaitForSeconds(m_stopSpan);
            m_rightLine.Move = false;
            yield return new WaitForSeconds(m_stopSpan);
            m_centerLine.TargetStop(m_rightLine.CrrentNum);
            yield return new WaitForSeconds(m_stopSpan);
            m_leftLine.TargetStop(m_rightLine.CrrentNum);
            CheckSlot();
        }
        IEnumerator SpanStop3()
        {
            Chack = true;
            yield return new WaitForSeconds(m_stopSpan);
            m_centerLine.Move = false;
            yield return new WaitForSeconds(m_stopSpan);
            m_leftLine.TargetStop(m_centerLine.CrrentNum);
            yield return new WaitForSeconds(m_stopSpan);
            m_rightLine.TargetStop(m_centerLine.CrrentNum);
            CheckSlot();
        }
        public void CheckSlot()
        {
            if (m_leftLine.Move || m_centerLine.Move || m_rightLine.Move)
            {
                return;
            }
            Chack = true;
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
            Chack = false;
            StopSlot?.Invoke();
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