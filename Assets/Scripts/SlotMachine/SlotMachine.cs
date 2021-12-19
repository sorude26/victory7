using System;
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
        [Header("１スロットの速度倍率")]
        [SerializeField]
        float m_oneSlotSpeed = 1f;
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
        [SerializeField]
        SEType m_stopSE = SEType.StopSpin;
        [SerializeField]
        LineControl m_lineControl = default;
        bool m_start = false;
        public bool CheckNow { get; private set; } = false;
        public bool Stop { get; private set; } = false;
        public bool SpinNow { get; private set; } = false;
        public event Action StopSlot;

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
                foreach (var item in SlotData.SevenSlotData)
                {
                    m_leftLine.SetSlot(item);
                }
                foreach (var item in SlotData.SevenSlotData)
                {
                    m_centerLine.SetSlot(item);
                }
                foreach (var item in SlotData.SevenSlotData)
                {
                    m_rightLine.SetSlot(item);
                }
            }
            else
            {
                foreach (var item in SlotData.LeftSlotData)
                {
                    m_leftLine.SetSlot(item);
                }
                foreach (var item in SlotData.CenterSlotData)
                {
                    m_centerLine.SetSlot(item);
                }
                foreach (var item in SlotData.RightSlotData)
                {
                    m_rightLine.SetSlot(item);
                }
            }
            m_leftLine.StopSE = m_stopSE;
            m_centerLine.StopSE = m_stopSE;
            m_rightLine.StopSE = m_stopSE;
            StartSlot();
            m_leftLine.Move = false;
            m_centerLine.Move = false;
            m_rightLine.Move = false;
        }
        private void Update()
        {
            if (BattleManager.Instance.BattleEnd)
            {
                return;
            }
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
                m_leftLine.SlotSpeed = m_oneSlotSpeed;
                m_centerLine.RotationTime = m_oneRotaionTime;
                m_centerLine.SlotSpeed = m_oneSlotSpeed;
                m_rightLine.RotationTime = m_oneRotaionTime;
                m_rightLine.SlotSpeed = m_oneSlotSpeed;
            }
            Stop = false;
            m_leftLine.StartSlot();
            m_centerLine.StartSlot();
            m_rightLine.StartSlot();
            if (m_start)
            {
                SoundManager.Play(SEType.StartSpin);
            }
            else
            {
                m_start = true;
            }
            m_lineControl.EndView();
        }
        public void SlotStartInput()
        {
            CheckNow = false;
            SpinNow = true;
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
            CheckNow = false;
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
            if (CheckNow || Stop || !SpinNow || !m_leftLine.Move)
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
            if (CheckNow || Stop || !SpinNow || !m_centerLine.Move)
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
            if (CheckNow || Stop || !SpinNow || !m_rightLine.Move)
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
            CheckNow = true;
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
            CheckNow = true;
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
            CheckNow = true;
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
            StartCoroutine(Check());
        }
        IEnumerator Check()
        {
            yield return new WaitForSeconds(0.2f);
            if (CheckDiagonalLineUP())
            {
                BattleManager.Instance.EffectActions.Push(()=> m_lineControl.PlayLine(4)); 
                m_leftLine.GetSlot(0).PlayEffect();
            }
            if (CheckDiagonalLineDown())
            {
                BattleManager.Instance.EffectActions.Push(() => m_lineControl.PlayLine(3));
                m_leftLine.GetSlot(2).PlayEffect();
            }
            if (CheckLine(0))
            {
                BattleManager.Instance.EffectActions.Push(() => m_lineControl.PlayLine(2));
                m_leftLine.GetSlot(0).PlayEffect();
            }
            if (CheckLine(2))
            {
                BattleManager.Instance.EffectActions.Push(() => m_lineControl.PlayLine(1));
                m_leftLine.GetSlot(2).PlayEffect();
            }
            if (CheckLine(1))
            {
                BattleManager.Instance.EffectActions.Push(() => m_lineControl.PlayLine(0));
                m_leftLine.GetSlot(1).PlayEffect();
            }
            yield return new WaitForSeconds(0.2f);
            SpinNow = false;
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
            if (left.Type == center.Type && left.EffectID == center.EffectID)
            {
                if (left.Type == right.Type && left.EffectID == right.EffectID)
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
            if (left.Type == center.Type && left.EffectID == center.EffectID)
            {
                if (left.Type == right.Type && left.EffectID == right.EffectID)
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
            if (left.Type == center.Type && left.EffectID == center.EffectID)
            {
                if (left.Type == right.Type && left.EffectID == right.EffectID)
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