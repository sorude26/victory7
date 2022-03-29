using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace victory7
{
    public class CharacterSelectController : MonoBehaviour
    {
        const int viewCharaCount = 4;
        [SerializeField]
        string m_target = "MapScene";
        [SerializeField]
        BGMType m_bgm = BGMType.Select;
        [SerializeField]
        PlayerParameter[] m_playerParameters = default;
        [SerializeField]
        StartSlotDataView m_dataViewPrefab = default;
        [SerializeField]
        RectTransform m_top = default;
        [SerializeField]
        RectTransform m_bottom = default;
        [SerializeField]
        float m_size = 400f;
        [SerializeField]
        float m_speed = 2f;
        [SerializeField]
        float m_bgmTime = 1f;
        [SerializeField]
        SkillPanel m_skillPanel = default;
        [SerializeField]
        SkillSelectMassage m_massage = default;
        [SerializeField]
        float m_dataScale = 0.3f;
        [SerializeField]
        Vector2 m_dataPos = Vector2.zero;
        GameObject[] m_characters = default;
        StartSlotDataView[] m_characterData = default;
        SkillPanel[] m_skillPanels = default;
        int m_selectNumber = 0;
        float m_posY = 0;
        bool m_move = false;
        bool m_up = false;
        bool m_select = false;
        bool m_massageOpen = false;
        bool m_inGame = false;
        void Start()
        {
            m_massage.CloseMassage();
            StartSet();
            m_inGame = true;
            SoundManager.PlayBGM(m_bgm, m_bgmTime);
            FadeController.Instance.StartFadeIn(() => m_inGame = false);
        }

        void Update()
        {
            if (m_inGame)
            {
                return;
            }
            if (Input.GetButtonDown("Vertical"))
            {
                if (m_massageOpen)
                {
                    return;
                }
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    if (!m_select)
                    {
                        m_up = true;
                        if (!m_move)
                        {
                            m_move = true;
                        }
                    }
                    else
                    {
                        SelectPanel().Back();
                    }
                }
                else
                {
                    if (!m_select)
                    {
                        m_up = false;
                        if (!m_move)
                        {
                            m_move = true;
                        }
                    }
                    else
                    {
                        SelectPanel().Next();
                    }
                }
            }
            else if (Input.GetButtonDown("Horizontal"))
            {
                if (m_massageOpen)
                {
                    m_massage.ChangeTarget();
                    return;
                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    if (m_select)
                    {
                        SelectPanel().Back();
                    }
                }
                else
                {
                    if (m_select)
                    {
                        SelectPanel().Next();
                    }
                }
            }
            if (Input.GetButtonDown("Cancel"))
            {
                if (m_massageOpen)
                {
                    m_massageOpen = false;
                    m_massage.CloseMassage();
                    SelectPanel().gameObject.SetActive(true);
                    int targetNum = GetSelectNum();
                    m_characterData[targetNum].gameObject.transform.localScale = Vector3.one;
                    m_characterData[targetNum].gameObject.transform.localPosition = Vector3.zero;
                    m_characterData[targetNum].gameObject.SetActive(false);
                    return;
                }
                if (m_select)
                {
                    m_select = false;
                    int targetNum = GetSelectNum();
                    m_characterData[targetNum].gameObject.SetActive(true);
                    m_skillPanels[targetNum].gameObject.SetActive(false);
                    foreach (var item in m_characters)
                    {
                        item.SetActive(true);
                    }
                    return;
                }
            }
            if (Input.GetButtonDown("Submit"))
            {
                if (m_massageOpen)
                {
                    if (m_massage.Decision())
                    {
                        m_massageOpen = false;
                        SelectPanel().gameObject.SetActive(true); 
                        int targetNum = GetSelectNum();
                        m_characterData[targetNum].gameObject.transform.localScale = Vector3.one;
                        m_characterData[targetNum].gameObject.transform.localPosition = Vector3.zero;
                        m_characterData[targetNum].gameObject.SetActive(false);
                    }
                    else
                    {
                        StartGame();
                        m_inGame = true;
                    }
                    return;
                }
                if (m_select)
                {
                    m_massage.Skill.SetData(SelectPanel().CurrentSkill);
                    m_massage.OpenMassage();
                    m_massageOpen = true;
                    SelectPanel().gameObject.SetActive(false);
                    int targetNum = GetSelectNum();
                    m_characterData[targetNum].gameObject.SetActive(true);
                    m_characterData[targetNum].gameObject.transform.localScale *= m_dataScale;
                    m_characterData[targetNum].gameObject.transform.localPosition = m_dataPos;
                    return;
                }
                if (!m_move)
                {
                    m_select = true;
                    foreach (var item in m_characters)
                    {
                        item.SetActive(false);
                    }
                    int targetNum = GetSelectNum();
                    m_characters[targetNum].gameObject.SetActive(true);
                    m_characterData[targetNum].gameObject.SetActive(false);
                    m_skillPanels[targetNum].gameObject.SetActive(true);
                    SelectPanel().SelectGuide();
                }
            }
            if (m_select)
            {
                return;
            }
            if (!m_move)
            {
                return;
            }
            if (m_up)
            {
                CharaMoveUp();
            }
            else
            {
                CharaMoveDown();
            }
        }
        void StartSet()
        {
            m_characters = new GameObject[m_playerParameters.Length];
            m_characterData = new StartSlotDataView[m_playerParameters.Length];
            m_skillPanels = new SkillPanel[m_playerParameters.Length];
            for (int i = 0; i < m_playerParameters.Length; i++)
            {
                var carabase = new GameObject("carabase");
                carabase.AddComponent<RectTransform>();
                carabase.transform.SetParent(transform);
                Instantiate(m_playerParameters[i].Character, carabase.transform);
                m_characters[i] = carabase;
                m_characters[i].transform.position = m_top.position + Vector3.up * m_size;
                m_characterData[i] = Instantiate(m_dataViewPrefab, transform);
                m_characterData[i].StartSet(m_playerParameters[i]);
                m_skillPanels[i] = Instantiate(m_skillPanel, transform);
                m_skillPanels[i].SetData(m_playerParameters[i].HaveSkills);
                m_skillPanels[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 3; i++)
            {
                m_characters[GetSelectNum(i)].transform.position = m_bottom.position + Vector3.up * m_size * i;
            }
            DataChange();
        }
        void CharaMoveDown()
        {
            m_posY -= m_size * m_characters.Length * m_speed * Time.deltaTime;
            CharaChange(() => m_posY < -m_size);
        }
        void CharaMoveUp()
        {
            m_posY += m_size * m_characters.Length * m_speed * Time.deltaTime;
            CharaChange(() => m_posY > m_size);
        }
        void CharaChange(Func<bool> changeCheck)
        {
            if (changeCheck())
            {
                m_posY = 0;
                m_characters[m_selectNumber].transform.position = m_top.position;
                m_selectNumber++;
                if (m_selectNumber >= m_characters.Length)
                {
                    m_selectNumber = 0;
                }
                DataChange();
                m_move = false;
            }
            for (int i = 0; i < viewCharaCount; i++)
            {
                m_characters[GetSelectNum(i)].transform.position = m_bottom.position + Vector3.up * m_size * i + new Vector3(0, m_posY, 0);
            }
        }
        void DataChange()
        {
            foreach (var item in m_characterData)
            {
                item.gameObject.SetActive(false);
            }
            m_characterData[GetSelectNum()].gameObject.SetActive(true);
        }
        SkillPanel SelectPanel()
        {
            return m_skillPanels[GetSelectNum()];
        }
        void StartGame()
        {
            FadeController.Instance.StartFadeOut(LoadGame);
            GameManager.Instance.StartSet(m_playerParameters[GetSelectNum()], SelectPanel().CurrentSkill);
            MapData.ClearReset();
        }
        int GetSelectNum()
        {
            if (m_selectNumber + 1 >= m_characterData.Length)
            {
                return 0;
            }
            return m_selectNumber + 1;
        }
        int GetSelectNum(int i)
        {
            if (m_selectNumber + i >= m_characterData.Length)
            {
                return m_selectNumber + i - m_characterData.Length;
            }
            return m_selectNumber + i;
        }
        void LoadGame()
        {
            SceneManager.LoadScene(m_target);
        }
    }
}