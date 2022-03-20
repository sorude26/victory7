using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace victory7
{
    public class MapPlayerData : MonoBehaviour
    {
        [SerializeField]
        Slider m_hpSlider = default;
        [SerializeField]
        Slider m_spSlider = default;
        void Start()
        {
            DataUpdate();
        }
        public void DataUpdate()
        {
            m_hpSlider.maxValue = PlayerData.MaxHP;
            m_hpSlider.value = PlayerData.CurrentHP;
            m_spSlider.maxValue = PlayerData.MaxSP;
            m_spSlider.value = PlayerData.CurrentSP;
        }
    }
}