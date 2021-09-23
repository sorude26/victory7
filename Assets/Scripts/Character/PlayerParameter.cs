using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace victory7
{
    [CreateAssetMenu]
    public class PlayerParameter : CharacterParameter
    {
        [SerializeField]
        int[] m_power = new int[3];
        [SerializeField]
        int[] m_guard = new int[3];
        [SerializeField]
        int[] m_charge = new int[3];
        [SerializeField]
        int[] m_heel = new int[3];
        public int[] Power { get => m_power; }
        public int[] Guard { get => m_guard; }
        public int[] Charge { get => m_charge; }
        public int[] Heel { get => m_heel; }
    }
}