using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using victory7;

public class RankManager : MonoBehaviour
{
    [SerializeField]
    [Header("表示するテキスト")]
    Text m_text;

    [SerializeField]
    [Header("表示する画像")]
    Image[] m_image = null;

    [SerializeField]
    bool m_flag = false;

    [SerializeField]
    float m_ChangeInterval = 2f;

    [SerializeField]
    TextChanger m_textChanger;

    [SerializeField]
    [Header("ランク設定時のしきい値")]
    int[] m_rankThreshold;

    [SerializeField]
    [Header("設定するランク")]
    string[] m_rankName;

    int m_score;

    string m_rank;

    private void OnEnable()
    {
        m_score = m_textChanger.Score;
    }

    private void Start()
    {
        if(m_flag)
        {
            m_image[CheckRank(m_rankThreshold, m_score)].enabled = true;
        }
        else
        {
            m_text.DOText("Rank" + m_rankName[CheckRank(m_rankThreshold, m_score)], m_ChangeInterval);
        }
    }

    int CheckRank(int[] array, int score)
    {
        for(int i = 0; i < array.Length; i++)
        {
            if(score > array[i])
            {
                if(i == array.Length - 1)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                return i;
            }
        }
        return array.Length;
    }
}
