using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EDataControl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_countBase = default;
    [SerializeField]
    private Text[] m_countTexts = default;

    public void StartSetCount(int[] counts)
    {
        foreach (var item in m_countBase)
        {
            item.SetActive(false);
        }
        m_countBase[counts.Length - 1].SetActive(true);
        SetCount(counts);
    }
    public void SetCount(int[] counts)
    {
        for (int i = 0; i < m_countTexts.Length; i++)
        {
            if (i < counts.Length)
            {
                m_countTexts[i].text = counts[i].ToString();
            }
            else
            {
                m_countTexts[i].text = default;
            }
        }
    }
}
