using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class CharacterSelectController : MonoBehaviour
    {
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
        GameObject[] m_characters = default;
        StartSlotDataView[] m_characterData = default;
        int m_selectNumber = 0;
        float m_posY = 0;
        bool m_move = false;
        bool m_up = false;
        void Start()
        {
            StartSet();
        }

        void Update()
        {
            if (Input.GetButtonDown("Vertical"))
            {
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    m_up = true;
                    if (!m_move)
                    {
                        m_move = true;
                    }
                }
                else
                {
                    m_up = false;
                    if (!m_move)
                    {
                        m_move = true;
                    }
                }
            }
            else if (Input.GetButtonDown("Horizontal"))
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    //m_currentEvent?.MoveLine(1);
                }
                else
                {
                    //m_currentEvent?.MoveLine(-1);
                }
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (!m_move)
                {
                    return;
                }
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
    }
}