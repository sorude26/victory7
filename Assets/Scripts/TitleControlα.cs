using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace victory7
{
    public class TitleControlα : MonoBehaviour
    {
        [SerializeField]
        string m_target = "MapScene";
        [SerializeField]
        BGMType m_bgm = BGMType.Title;
        [SerializeField]
        GameObject m_cursor = default;
        [SerializeField]
        Transform[] m_cursorPos = default;
        [SerializeField]
        OptionControl m_control = default;

        private bool m_isStart = false;
        private bool m_isOption = false;
        private SelectType m_cursorTarget = SelectType.StartGame;

        private enum SelectType
        {
            StartGame,
            Option,
            Quit,
        }
        void Start()
        {
            SoundManager.PlayBGM(m_bgm);
            FadeController.Instance.StartFadeIn(() => 
            { 
                m_isStart = true;
            });
            m_control.OnEventEnd += () => m_isOption = false;
            MapData.ClearReset();
        }

        void Update()
        {
            if (!m_isStart)
            {
                return;
            }
            if (Input.GetButtonDown("Vertical"))
            {
                if (m_isOption)
                {
                    m_control.MoveSlot(1);
                    return;
                }
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    ChangeSelect(-1);
                }
                else
                {
                    ChangeSelect(1);
                }
            }
            else if (Input.GetButtonDown("Horizontal") && m_isOption)
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    m_control.MoveLine(1);
                }
                else
                {
                    m_control.MoveLine(-1);
                }
                return;
            }
            if (Input.GetButtonDown("Submit"))
            {
                if (m_isOption)
                {
                    return;
                }
                switch (m_cursorTarget)
                {
                    case SelectType.StartGame:
                        GamaStart();
                        break;
                    case SelectType.Option:
                        m_isOption = true;
                        m_control.SelectEvent();
                        break;
                    case SelectType.Quit:
                        m_isStart = false;
                        SoundManager.StopBGM();
                        FadeController.Instance.StartFadeOut(Quit);
                        break;
                    default:
                        break;
                }
                SoundManager.Play(SEType.Decision);
                return;
            }
            if (Input.GetButtonDown("Cancel"))
            {
                if (m_isOption)
                {
                    m_control.CancelAction();
                    return;
                }
            }
        }
        public void GamaStart()
        {
            if (!m_isStart)
            {
                return;
            }
            m_isStart = false;
            SoundManager.Play(SEType.Decision);
            FadeController.Instance.StartFadeOut(LoadMap);
        }
        void LoadMap()
        {
            SceneManager.LoadScene(m_target);
        }
        void ChangeSelect(int dir)
        {
            int current = (int)m_cursorTarget;
            int max = Enum.GetValues(typeof(SelectType)).Length;
            if (dir > 0)
            {
                current++;
                if (current >= max)
                {
                    current = 0;
                }
            }
            else if (dir < 0)
            {
                current--;
                if (current < 0) 
                { 
                    current = max - 1; 
                }
            }
            m_cursorTarget = (SelectType)current;
            m_cursor.transform.localPosition = m_cursorPos[current].localPosition;
            SoundManager.Play(SEType.Choice);
        }
        void Quit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
            #endif
        }
    }
}
