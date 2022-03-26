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

    [SerializeField]
    [Header("スコア計算用の値(マップクリア数)")]
    int m_mapCalculation = 2500;

    [SerializeField]
    [Header("スコア計算用の値(バトルクリア数)")]
    int m_victoryCalculation = 100;

    int m_score;

    private void OnEnable()
    {
        m_score = Calculation();
    }

    private void Start()
    {
        m_image[CheckRank(m_rankThreshold, m_score)].enabled = true;
    }

    int Calculation()
    {
        int s = default;
        for (int i = 0; i < MapData.BattleCount; i++)
        {
            s += m_victoryCalculation;
        }
        
        if(MapData.ClearStageCount > 0)
        {
            s += 10000;
            if(MapData.ClearStageCount > 1)
            {
                for (int i = 0; i < MapData.ClearStageCount; i++)
                {
                    s += m_mapCalculation;
                }
            }
        }
        return s;
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
