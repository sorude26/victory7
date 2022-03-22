using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using victory7;

public class BGChanger : MonoBehaviour
{
    [SerializeField]
    Image m_sp;

    void Start()
    {
        m_sp.sprite = MapData.CurrentMap.BattleBackground;
    }
}
