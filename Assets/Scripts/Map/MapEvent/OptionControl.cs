using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class OptionControl : MapEventControlBase
    {
        const int SoundScale = 100;
        [SerializeField]
        private Slider m_bgmSlider = default;
        [SerializeField]
        private Slider m_seSlider = default;
        [SerializeField]
        private float m_moveInterval = 0.1f;
        [SerializeField]
        private float m_inputInterval = 0.7f;
        private int m_bgmVolume = default;
        private int m_seVolume = default;
        private TargetSlider m_target = default;
        float m_inputTimer = 0f;
        float m_moveTimer = 0f;
        private enum TargetSlider 
        { 
            BGM,
            SE,
        }

        public override void StartSet()
        {
            m_selectMark.position = m_bgmSlider.transform.position;
            m_rect = GetComponent<RectTransform>();
            m_startPos = m_rect.position;
            m_rect.position = m_hidePos;
            m_bgmSlider.value = SoundManager.BGMVolume * SoundScale;
            m_seSlider.value = SoundManager.SEVolume * SoundScale;
            m_bgmVolume = (int)m_bgmSlider.value;
            m_seVolume = (int)m_seSlider.value;
        }
        private void Update()
        {
            if (!m_select)
            {
                return;
            }
            if (Input.GetButton("Horizontal"))
            {
                if (m_inputTimer < m_inputInterval)
                {
                    m_inputTimer += Time.deltaTime;
                    return;
                }
                if (m_moveTimer < m_moveInterval)
                {
                    m_moveTimer += Time.deltaTime;
                    return;
                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    MoveLine(1);
                }
                else
                {
                    MoveLine(-1);
                }
                m_moveTimer = 0;
            }
            else if (m_inputTimer > 0)
            {
                m_inputTimer = 0;
            }
        }
        public override void SelectAction()
        {
            return;
        }
        public override void CancelAction()
        {
            SoundManager.Play(SEType.Cancel);
            OutSelectEvent();
        }
        public override void MoveLine(int dir)
        {
            if (dir > 0)
            {
                TargetVolumeUp();
            }
            else if (dir < 0)
            {
                TargetVolumeDown();
            }
            SoundManager.Play(SEType.Choice);
            return;
        }
        public override void MoveSlot(int dir)
        {
            switch (m_target)
            {
                case TargetSlider.BGM:
                    m_target = TargetSlider.SE;
                    m_selectMark.position = m_seSlider.transform.position;
                    break;
                case TargetSlider.SE:
                    m_target = TargetSlider.BGM;
                    m_selectMark.position = m_bgmSlider.transform.position;
                    break;
                default:
                    break;
            }
            SoundManager.Play(SEType.Choice);
            return;
        }
        
        private void TargetVolumeUp()
        {
            switch (m_target)
            {
                case TargetSlider.BGM:
                    m_bgmVolume++;
                    break;
                case TargetSlider.SE:
                    m_seVolume++;
                    break;
                default:
                    break;
            }
            SetVolume(m_target);
        }
        private void TargetVolumeDown()
        {
            switch(m_target)
            {
                case TargetSlider.BGM:
                    m_bgmVolume--;
                    break;
                case TargetSlider.SE:
                    m_seVolume--;
                    break;
                default:
                    break;
            }
            SetVolume(m_target);
        }
        private void SetVolume(TargetSlider target)
        {
            switch (target)
            {
                case TargetSlider.BGM:
                    if (m_bgmVolume < 0)
                    {
                        m_bgmVolume = 0;
                    }
                    else if(m_bgmVolume > SoundScale)
                    {
                        m_bgmVolume = SoundScale;
                    }
                    SoundManager.BGMVolume = (float)m_bgmVolume / SoundScale;
                    m_bgmSlider.value = SoundManager.BGMVolume * SoundScale;
                    break;
                case TargetSlider.SE:
                    if (m_seVolume < 0)
                    {
                        m_seVolume = 0;
                    }
                    else if (m_seVolume > SoundScale)
                    {
                        m_seVolume = SoundScale;
                    }
                    SoundManager.SEVolume = (float)m_seVolume / SoundScale;
                    m_seSlider.value = SoundManager.SEVolume * SoundScale;
                    break;
                default:
                    break;
            }
        }
    }
}