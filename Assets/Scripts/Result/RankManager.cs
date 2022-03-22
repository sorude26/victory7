using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using victory7;

public class RankManager : MonoBehaviour
{
    [SerializeField]
    [Header("表示する画像")]
    Image[] m_image = null;

    [SerializeField]
    TextChanger m_textChanger;

    [SerializeField]
    [Header("ランク設定時のしきい値")]
    int[] m_rankThreshold;

    int m_score;

    private void OnEnable()
    {
        m_score = m_textChanger.Score;
    }

    private void Start()
    {
        m_image[CheckRank(m_rankThreshold, m_score)].enabled = true;
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
