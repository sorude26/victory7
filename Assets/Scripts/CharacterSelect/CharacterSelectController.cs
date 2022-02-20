using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace victory7
{
    public class CharacterSelectController : MonoBehaviour
    {
        [SerializeField]
        string m_target = "MapScene";
        [SerializeField]
        PlayerParameter[] m_players = default;
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
        SkillPanel m_skillPanel = default;
        [SerializeField]
        SkillDataLibrary m_dataLibrary = default;
        [SerializeField]
        SkillSelectMassage m_massage = default;
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
                    return;
                }
                if (m_select)
                {
                    m_select = false;
                    if (m_selectNumber + 1 < m_characterData.Length)
                    {
                        m_skillPanels[m_selectNumber + 1].gameObject.SetActive(false);
                        m_characterData[m_selectNumber + 1].gameObject.SetActive(true);
                    }
                    else
                    {
                        m_characterData[0].gameObject.SetActive(true);
                        m_skillPanels[0].gameObject.SetActive(false);
                    }
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
                    return;
                }
                if (!m_move)
                {
                    m_select = true; 
                    foreach (var item in m_characters)
                    {
                        item.SetActive(false);
                    }
                    if (m_selectNumber + 1 < m_characterData.Length)
                    {
                        m_characters[m_selectNumber + 1].gameObject.SetActive(true);
                        m_skillPanels[m_selectNumber + 1].gameObject.SetActive(true);
                        m_characterData[m_selectNumber + 1].gameObject.SetActive(false);
                    }
                    else
                    {
                        m_characters[0].gameObject.SetActive(true);
                        m_characterData[0].gameObject.SetActive(false);
                        m_skillPanels[0].gameObject.SetActive(true);
                    }
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
            m_characters = new GameObject[m_players.Length];
            m_characterData = new StartSlotDataView[m_players.Length];
            m_skillPanels = new SkillPanel[m_players.Length];
            for (int i = 0; i < m_players.Length; i++)
            {
                var carabase = new GameObject("carabase");
                carabase.AddComponent<RectTransform>();
                carabase.transform.SetParent(transform);
                Instantiate(m_players[i].Character, carabase.transform);
                m_characters[i] = carabase;
                m_characters[i].transform.position = m_top.position + Vector3.up * m_size;
                m_characterData[i] = Instantiate(m_dataViewPrefab, transform);
                m_characterData[i].StartSet(m_players[i]);
                m_skillPanels[i] = Instantiate(m_skillPanel, transform);
                m_skillPanels[i].SetData(m_players[i].HaveSkills);
                m_skillPanels[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 3; i++)
            {
                if (i + m_selectNumber >= m_characters.Length)
                {
                    m_characters[i + m_selectNumber - m_characters.Length].transform.position = m_bottom.position + Vector3.up * m_size * i;
                }
                else
                {
                    m_characters[i + m_selectNumber].transform.position = m_bottom.position + Vector3.up * m_size * i;
                }
            }
            DataChange();
        }
        void CharaMoveDown()
        {
            m_posY -= m_size * m_characters.Length * m_speed * Time.deltaTime;
            if (m_posY < -m_size)
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
            for (int i = 0; i < 4; i++)
            {
                if (i + m_selectNumber >= m_characters.Length)
                {
                    m_characters[i + m_selectNumber - m_characters.Length].transform.position = m_bottom.position + Vector3.up * m_size * i + new Vector3(0, m_posY, 0);
                }
                else
                {
                    m_characters[i + m_selectNumber].transform.position = m_bottom.position + Vector3.up * m_size * i + new Vector3(0, m_posY, 0);
                }
            }
        }
        void CharaMoveUp()
        {
            m_posY += m_size * m_characters.Length * m_speed * Time.deltaTime;
            if (m_posY > m_size)
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
            for (int i = 0; i < 4; i++)
            {
                if (i + m_selectNumber >= m_characters.Length)
                {
                    m_characters[i + m_selectNumber - m_characters.Length].transform.position = m_bottom.position + Vector3.up * m_size * i + new Vector3(0, m_posY, 0);
                }
                else
                {
                    m_characters[i + m_selectNumber].transform.position = m_bottom.position + Vector3.up * m_size * i + new Vector3(0, m_posY, 0);
                }
            }
        }
        void DataChange()
        {
            foreach (var item in m_characterData)
            {
                item.gameObject.SetActive(false);
            }
            if (m_selectNumber + 1 < m_characterData.Length)
            {
                m_characterData[m_selectNumber + 1].gameObject.SetActive(true);
            }
            else
            {
                m_characterData[0].gameObject.SetActive(true);
            }
        }
        SkillPanel SelectPanel()
        {
            if (m_selectNumber + 1 < m_characterData.Length)
            {
                return m_skillPanels[m_selectNumber + 1];
            }
            else
            {
                return m_skillPanels[0];
            }
        }
        void StartGame()
        {
            FadeController.Instance.StartFadeOut(LoadGame);
            if (m_selectNumber + 1 < m_characterData.Length)
            {
                GameManager.Instance.StartSet(m_players[m_selectNumber + 1],SelectPanel().CurrentSkill);
            }
            else
            {
                GameManager.Instance.StartSet(m_players[0], SelectPanel().CurrentSkill);
            }
            MapData.ClearReset();
        }
        void LoadGame()
        {
            SceneManager.LoadScene(m_target);
        }
    }
}